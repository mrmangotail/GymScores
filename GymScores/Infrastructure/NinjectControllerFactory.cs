using System;
using System.Web.Mvc;
using System.Web.Routing;
using GymScores.Domain.Abstract;
using GymScores.Domain.Concrete;
using Ninject;

namespace GymScores.Infrastructure
{
    public class NinjectControllerFactory : DefaultControllerFactory
    {
        private IKernel ninjectKernal;

        public NinjectControllerFactory()
        {
            ninjectKernal = new StandardKernel();
            AddBindings();
        }

        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            return controllerType == null
                ? null
                : (IController)ninjectKernal.Get(controllerType);
        }

        private void AddBindings()
        {
            ninjectKernal.Bind<IMeetRepository>().To<EFMeetRepository>();
            ninjectKernal.Bind<IGymnastRepository>().To<EFGymnastRepository>();
            ninjectKernal.Bind<IScoreRepository>().To<EFScoreRepository>();
            ninjectKernal.Bind<IAuthProvider>().To<FormsAuthProvider>();
        }
    }
}