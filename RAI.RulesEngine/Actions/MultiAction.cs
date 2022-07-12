using RulesEngine.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RulesEngine.Actions
{
    public class MultiAction : ActionBase
    {
        private readonly ActionFactory _actionFactory;

        public MultiAction(IDictionary<string, Func<ActionBase>> actionMap)
        {
            _actionFactory = new ActionFactory(actionMap);
        }

        public override async ValueTask<object> Run(ActionContext context, RuleParameter[] ruleParameters)
        {
            var actionInfos = context.GetContext<List<ActionInfo>>("Actions");
            var results = new List<object>();

            foreach (var actionInfo in actionInfos)
            {
                var action = _actionFactory.Get(actionInfo.Name);
                var resultTree = context.GetParentRuleResult();
                //var ruleParameters = context.GetParentRuleResult().Inputs.Select(kv => new RuleParameter(kv.Key, kv.Value)).ToArray();
                var result = await action.ExecuteAndReturnResultAsync(new ActionContext(actionInfo.Context, resultTree), ruleParameters, true);

                results.Add(result);
            }

            return new ValueTask<object>(results.AsReadOnly());
        }
    }
}
