using Sitecore.GraphQL.Authenticator.Authenticator;
using Sitecore.GraphQL.ClientXamarin.GraphQLModel;
using Sitecore.GraphQL.ClientXamarin.Models;
using Sitecore.GraphQL.Services.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using ViewItem = Sitecore.GraphQL.ClientXamarin.Models;
using GraphItem = Sitecore.GraphQL.ClientXamarin.GraphQLModel;
using GraphQLParser;
using Newtonsoft.Json;

namespace Sitecore.GraphQL.ClientXamarin.Services
{
    public class MockDataStore : IDataStore<ViewItem.Item>
    {
        readonly List<ViewItem.Item> items;

        public MockDataStore()
        {
            if (!string.IsNullOrEmpty(Login.AuthToken))
            {
                var graphQLQuery = "{item(path: \"/sitecore/content/Home\") {" +
                                        @"id
                                          name
                                        fields(excludeStandardFields: true) {
                                          id
                                          name
                                          value
                                          " + "rendered(fieldRendererParameters: \"w=100&h=160\")" + @"
                                        }
                                        children {
                                            id
                                          name
                                          fields(excludeStandardFields: true) {
                                            id
                                            name
                                            value
                                            " + "rendered(fieldRendererParameters: \"w=100&h=160\")" + @"
                                          }
                                        }
                                      }
                                    }";

                AllItemsGraphQLModel graphQLData = ReadService.FetchGraphQLData<AllItemsGraphQLModel>(Login.AuthToken, Login.SCC_GRAPHURL, Login.siteDomain, graphQLQuery);

                // Add Home item 
                items = new List<ViewItem.Item>() { };

                items.Add(new ViewItem.Item { 
                    Id = graphQLData.item.id, Text = graphQLData.item.name, 
                    Description = graphQLData.item.fields.Where(x => x.name.Equals("Text",StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault()?.value 
                });

                Func<string, string> getSRCFromImage = (x) =>
                 {
                     // Get the index of where the value of src starts.
                     string str = x;
                     int start = str.IndexOf("<img src=\"") + 10;

                     // Get the substring that starts at start, and goes up to first \".
                     string src = str.Substring(start, str.IndexOf("\"", start) - start);

                     return src;
                 };

                // Add child items

                foreach (Child child in graphQLData.item.children)
                {
                    items.Add(new ViewItem.Item
                    {
                        Id = child.id,
                        Text = child.name,
                        Description = child.fields.Where(x => x.name.Equals("Text", StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault()?.value,
                        ImageSRC = getSRCFromImage(child.fields.Where(x => x.name.Equals("BannerImage", StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault()?.rendered.Replace("/-/", "https://" + Login.siteDomain + "/-/"))
                    });
                }
            }

            /*items = new List<ViewItem.Item>()
            {
                new ViewItem.Item { Id = Guid.NewGuid().ToString(), Text = "First item", Description="This is an item description." },
                new ViewItem.Item { Id = Guid.NewGuid().ToString(), Text = "Second item", Description="This is an item description." },
                new ViewItem.Item { Id = Guid.NewGuid().ToString(), Text = "Third item", Description="This is an item description." },
                new ViewItem.Item { Id = Guid.NewGuid().ToString(), Text = "Fourth item", Description="This is an item description." },
                new ViewItem.Item { Id = Guid.NewGuid().ToString(), Text = "Fifth item", Description="This is an item description." },
                new ViewItem.Item { Id = Guid.NewGuid().ToString(), Text = "Sixth item", Description="This is an item description." }
            };
            */
        }

        public async Task<bool> AddItemAsync(ViewItem.Item item)
        {
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(ViewItem.Item item)
        {
            var oldItem = items.Where((ViewItem.Item arg) => arg.Id == item.Id).FirstOrDefault();
            items.Remove(oldItem);
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            var oldItem = items.Where((ViewItem.Item arg) => arg.Id == id).FirstOrDefault();
            items.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<ViewItem.Item> GetItemAsync(string id)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<ViewItem.Item>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(items);
        }
    }
}