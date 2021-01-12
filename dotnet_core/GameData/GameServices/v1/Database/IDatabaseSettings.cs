using System;
using System.Collections.Generic;
using System.Text;

namespace GameServices.v1.Database
{
    public interface IDatabaseSettings
    {
        string CollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
