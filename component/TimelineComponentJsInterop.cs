using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace timeline_component
{
    // This class can be registered as scoped DI service and then injected into Blazor
    // components for use.

    public class TimelineComponentJsInterop : IAsyncDisposable
    {
        private readonly Lazy<Task<IJSObjectReference>> moduleTask;
        private const string GetBoundingClientFunction = "getBoundingClient";
        private const string SetVisibilityFunction = "setVisibility";
        private const string SetTranslateYFunction = "setTranslateY";
        private const string GetClientHeightFunction = "getClientHeight";

        public TimelineComponentJsInterop(IJSRuntime jsRuntime)
        {
            moduleTask = new(() => jsRuntime.InvokeAsync<IJSObjectReference>(
               "import", "./_content/timeline-component/timelineComponentJsInterop.js").AsTask());
        }

        public async ValueTask<double[]> GetBoundingClient(ElementReference element)
        {
            var module = await moduleTask.Value;
            var result = await module.InvokeAsync<double[]>(GetBoundingClientFunction, element);
            if (result.Length != 2)
            {
                throw new InvalidOperationException("Number of parameters");
            }
            return result;
        }

        public async ValueTask<double> GetClientHeight(ElementReference element)
        {
            var module = await moduleTask.Value;
            return await module.InvokeAsync<double>(GetClientHeightFunction, element);
        }

        public async ValueTask SetVisibility(ElementReference element, bool value)
        {
            var module = await moduleTask.Value;
            await module.InvokeVoidAsync(SetVisibilityFunction, element, value);
        }

        public async ValueTask SetTranslateY(ElementReference element, double value)
        {
            var module = await moduleTask.Value;
            await module.InvokeVoidAsync(SetTranslateYFunction, element, value);
        }

        public async ValueTask DisposeAsync()
        {
            if (moduleTask.IsValueCreated)
            {
                var module = await moduleTask.Value;
                await module.DisposeAsync();
            }
        }
    }
}
