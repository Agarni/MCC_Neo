using MCC_Neo.Core.ViewModels;
using MvvmCross.IoC;
using MvvmCross.ViewModels;

namespace MCC_Neo.Core
{
    public class App : MvxApplication
    {
        public static int IdCursilho { get; set; }
        public static string DescricaoCursilho { get; set; }

        public override void Initialize()
        {
            CreatableTypes()
                .EndingWith("Service")
                .AsInterfaces()
                .RegisterAsLazySingleton();

            RegisterAppStart<HomeViewModel>();
        }
    }
}
