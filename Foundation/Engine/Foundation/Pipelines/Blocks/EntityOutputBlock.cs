using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Sitecore.Commerce.Core;
using Sitecore.Framework.Pipelines;

namespace Achievecreative.Commerce.Plugin.Foundation.Pipelines.Blocks
{
    public class EntityOutputBlock<T> : AsyncPipelineBlock<T, T, CommercePipelineExecutionContext> where T : class
    {
        public override Task<T> RunAsync(T arg, CommercePipelineExecutionContext context)
        {
            var text = $"Type: {typeof(T)}. Data is NULL";
            if (!(arg is null))
            {
                text = JsonConvert.SerializeObject(arg, Formatting.Indented, new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                    NullValueHandling = NullValueHandling.Ignore
                });
            }

            context.CommerceContext.Logger.LogWarning(text);

            return Task.FromResult(arg);
        }
    }
}
