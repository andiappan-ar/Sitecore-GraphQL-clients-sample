using System;
using System.Collections.Generic;
using System.Text;

namespace Sitecore.GraphQL.ClientXamarin.GraphQLModel
{

    public class Field
    {
        public string id { get; set; }
        public string name { get; set; }
        public string value { get; set; }
    }

    public class Item
    {
        public string name { get; set; }
        public List<Field> fields { get; set; }
    }

    public class Data
    {
        public Item item { get; set; }
    }

    public class AboutGraphQLModel
    {
        public Data data { get; set; }
    }

}
