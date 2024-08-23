using Autofac;
using Prism.Autofac;
using SimpleHmi.PlcService;
using SimpleHmi.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SimpleHmi
{
    class Bootstrapper : AutofacBootstrapper
    {
        protected override DependencyObject CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void InitializeShell()
        {
            Application.Current.MainWindow.Show();
        }

        protected override void ConfigureContainerBuilder(ContainerBuilder builder)
        {
            base.ConfigureContainerBuilder(builder);
            //builder.RegisterType<S7PlcService>().As<IPlcService>().SingleInstance();
            builder.RegisterType<ABPlcService>().As<IPlcService>().SingleInstance();
            builder.RegisterTypeForNavigation<MainPage>("MainPage");
            builder.RegisterTypeForNavigation<LeftMenu>("LeftMenu");
            builder.RegisterTypeForNavigation<MCP01Page>("MCP01Page");
            builder.RegisterTypeForNavigation<MCP02Page>("MCP02Page");
            builder.RegisterTypeForNavigation<MCP03Page>("MCP03Page");
            builder.RegisterTypeForNavigation<MCP04Page>("MCP04Page");
            builder.RegisterTypeForNavigation<MCP05Page>("MCP05Page");
            builder.RegisterTypeForNavigation<MCP06Page>("MCP06Page");
            builder.RegisterTypeForNavigation<MCP07Page>("MCP07Page");
        }
    }
}
