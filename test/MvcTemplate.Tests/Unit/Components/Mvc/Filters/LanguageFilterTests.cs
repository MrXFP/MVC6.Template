﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using MvcTemplate.Components.Mvc;
using NSubstitute;
using Xunit;

namespace MvcTemplate.Tests.Unit.Components.Mvc
{
    public class LanguageFilterTests
    {
        #region OnResourceExecuting(ResourceExecutingContext context)

        [Fact]
        public void OnActionExecuting_SetsCurrentLanguage()
        {
            ActionContext action = new ActionContext(new DefaultHttpContext(), new RouteData(), new ActionDescriptor());
            ResourceExecutingContext context = new ResourceExecutingContext(action, new IFilterMetadata[0]);
            ILanguages languages = Substitute.For<ILanguages>();
            context.RouteData.Values["language"] = "lt";
            languages["lt"].Returns(new Language());

            new LanguageFilter(languages).OnResourceExecuting(context);

            Language actual = languages.Current;
            Language expected = languages["lt"];

            Assert.Equal(expected, actual);
        }

        #endregion
    }
}