using Godsend.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace API.Tests.Common
{
    public static class TestHelper
    {
        public static DataContext GetInMemoryContext()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                      .UseInMemoryDatabase(Guid.NewGuid().ToString())
                      .Options;

            var context = new DataContext(options);

            return context;
        }
    }
}
