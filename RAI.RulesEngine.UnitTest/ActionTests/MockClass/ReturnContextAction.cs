// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using RulesEngine.Actions;
using RulesEngine.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RulesEngine.UnitTest.ActionTests.MockClass
{
    public class ReturnContextAction : ActionBase
    {
        public override Task<object> Run(ActionContext context, RuleParameter[] ruleParameters)
        {
            var stringContext = context.GetContext<string>("stringContext");
            var intContext = context.GetContext<int>("intContext");
            var objectContext = context.GetContext<object>("objectContext");

            return Task.FromResult<dynamic>(new {
                stringContext,
                intContext,
                objectContext
            });
        }
    }
}
