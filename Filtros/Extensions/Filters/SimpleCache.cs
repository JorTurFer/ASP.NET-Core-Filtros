using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Filtros.Extensions.Filters
{
    class CacheEntry
    {
        public DateTime CachedAt { get; set; }
        public ContentResult Content { get; set; }
    }
    public class SimpleCacheAttribute : Attribute, IResourceFilter
    {
        private static readonly Dictionary<string, CacheEntry>
            _cache = new Dictionary<string, CacheEntry>();

        private string _cacheKey;


        // Antes de ejecutar la acción se comprueba si tenemos el resultado en caché
        // retornándolo en caso afirmativo
        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            _cacheKey = context.HttpContext.Request.Path.ToString();
            if (_cache.ContainsKey(_cacheKey))
            {
                var cacheEntry = _cache[_cacheKey] as CacheEntry;
                if (cacheEntry != null)
                {
                    if (DateTime.Now.Subtract(cacheEntry.CachedAt) > TimeSpan.FromSeconds(10))
                    {
                        _cache.Remove(_cacheKey);
                    }
                    else
                    {
                        context.Result = cacheEntry.Content;
                    }
                }
            }
        }

        // Tras ejecutar la acción, guardamos en el diccionario los ContentResult
        // para usos posteriores
        public void OnResourceExecuted(ResourceExecutedContext context)
        {
            if (!String.IsNullOrEmpty(_cacheKey) &&
                !_cache.ContainsKey(_cacheKey))
            {
                var result = context.Result as ContentResult;
                if (result != null)
                {
                    var cacheEntry = new CacheEntry()
                    {
                        CachedAt = DateTime.Now,
                        Content = result
                    };
                    _cache.Add(_cacheKey, cacheEntry);
                }
            }
        }
    }
}
