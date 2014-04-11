using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Domain.Abstract;
using Domain.Concrete;
using Ninject;
using WebUI.Infrastructure.Abstract;
using WebUI.Infrastructure.Concrete;

namespace WebUI.Infrastructure
{
    public class NinjectControllerFactory:DefaultControllerFactory
    {
         private IKernel _ninjectKernel;

        public NinjectControllerFactory()
        {
            _ninjectKernel = new StandardKernel();
            AddBindings();
        }

        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {

            return controllerType == null ? null : (IController)_ninjectKernel.Get(controllerType);

        }

        private void AddBindings()
        {
            _ninjectKernel.Bind<IProductRepository>().To<EfProductRepository>();
            _ninjectKernel.Bind<IAuthProvider>().To<FormAuthProvider>();
            _ninjectKernel.Bind<IUserRepository>().To<EfUserRepository>();
        }

    }
}