using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Adaca.Api.Extentions
{
    public static class PipeExtenstion
    {

        public static IActionResult MatchToActionResult<T>(this Result<T> result, Func<T,IActionResult> okFunc, Func<string, IActionResult> errorFunc)
        {
            return result.IsSuccess ? okFunc(result.Value) : errorFunc(result.Error);
        }

        public static IActionResult MatchToActionResult(this Result result, Func<IActionResult> okFunc, Func<string, IActionResult> errorFunc)
        {
            return result.IsSuccess ? okFunc() : errorFunc(result.Error);
        }

        public static T2 Pipe<T1, T2>(this T1 extended, Func<T1, T2> func1)
        {
            return func1(extended);
        }

        public static T3 Pipe<T1, T2,T3>(this T1 extended, Func<T1, T2> func1, Func<T2, T3> func2)
        {
            return func2(func1(extended));
        }

        public static T4 Pipe<T1, T2, T3,T4>(this T1 extended, Func<T1, T2> func1, Func<T2, T3> func2, Func<T3, T4> func3)
        {
            return func3(func2(func1(extended)));
        }

    }
}
