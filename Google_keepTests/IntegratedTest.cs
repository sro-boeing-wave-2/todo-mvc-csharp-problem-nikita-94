using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using NSuperTest;

namespace Google_keepTests
{
    public class IntegratedTest
    {
        static Server server;
        public static void Init(TestContext ctx)
        {
            server = new Server();
        }

        // [Fact]
        public IntegratedTest()
        {
            //static Server<Startup> server;
            server = new Server("https://localhost:44367");
        }
        [Fact]
        public void Gettest()
        {
            server.Get("api/Notes1").Expect(200).End();
        }
    }

    public class TestContext
    {
    }
}

