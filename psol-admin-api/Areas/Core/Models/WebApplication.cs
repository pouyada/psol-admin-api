using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication
{
    public class NamespaceConstraint : ActionMethodSelectorAttribute
    {
        public override bool IsValidForRequest(RouteContext routeContext, ActionDescriptor action)
        {
            var dataTokenNamespace = (string)routeContext.RouteData.DataTokens.FirstOrDefault(dt => dt.Key == "Namespace").Value;
            var actionNamespace = ((ControllerActionDescriptor)action).MethodInfo.DeclaringType.FullName;

            return dataTokenNamespace == actionNamespace;
        }
    }
}
