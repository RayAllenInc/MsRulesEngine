// Copyright (c) Microsoft Corporation.
//  Licensed under the MIT License.

using RulesEngine.ExpressionBuilders;
using RulesEngine.Models;
using System.Threading.Tasks;

namespace RulesEngine.Actions
{
    public class OutputExpressionAction : ActionBase
    {
        private readonly RuleExpressionParser _ruleExpressionParser;

        public OutputExpressionAction(RuleExpressionParser ruleExpressionParser)
        {
            _ruleExpressionParser = ruleExpressionParser;
        }

        public override Task<object> Run(ActionContext context, RuleParameter[] ruleParameters)
        {
            var expression = context.GetContext<string>("expression");
            return Task.FromResult(_ruleExpressionParser.Evaluate<object>(expression, ruleParameters));
        }
    }
}
