using Autofac;
using PolyStock.Setup;
using PolyStock.SqlData;

namespace PolyStock.Console.Setup
{
    public class Bootstrapper : Module
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="builder"></param>


        public static void Initialize()
        {
            IOC.Instance.Setup(new[] { typeof(ServicesModule), typeof(ViewModelModule), typeof(Bootstrapper),typeof(SQlCovidModule),typeof(SQLiteBootstrapper) });
        }
    }
}