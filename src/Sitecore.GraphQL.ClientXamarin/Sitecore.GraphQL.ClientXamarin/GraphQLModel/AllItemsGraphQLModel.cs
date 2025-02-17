﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Sitecore.GraphQL.ClientXamarin.GraphQLModel
{

    public class Field
    {
        public string id { get; set; }
        public string name { get; set; }
        public string value { get; set; }
        public string rendered { get; set; }
    }

    public class Child
    {
        public string id { get; set; }
        public string name { get; set; }
        public List<Field> fields { get; set; }
    }

    public class Item
    {
        public string id { get; set; }
        public string name { get; set; }
        public List<Field> fields { get; set; }
        public List<Child> children { get; set; }
    }

    public class AllItemsGraphQLModel
    {
     
        public Item item { get; set; }
    }   


}
