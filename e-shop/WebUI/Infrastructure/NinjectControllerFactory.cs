using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Domain;
using Domain.Abstract;
using Domain.Concrete;
using Domain.Parameters;
using Ninject;


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
            _ninjectKernel.Bind<IUserRepository>().To<EfUserRepository>();
            _ninjectKernel.Bind<IAddressRepository>().To<EfAddressRepository>();
            _ninjectKernel.Bind<IAddressCustomerRepository>().To<EFCustomerAddressRepository>();
            _ninjectKernel.Bind<IProductCategoryRepository>().To<EFProductCategoryRepository>();
            _ninjectKernel.Bind<ISalesOrderDetail>().To<EFSalesOrderDetails>();
            _ninjectKernel.Bind<ISalesOrderHeader>().To<EFSalesOrderHeader>();
            _ninjectKernel.Bind<IOrderProcessor>().To<OrderProcessor>();
            _ninjectKernel.Bind<IPayment<CardParameters>>().To<PaymentCard>();
            _ninjectKernel.Bind<IProductModelRepository>().To<EFProductModelRepository>();
            _ninjectKernel.Bind<IProductModelProductDescription>().To<EFProductModelProductDescription>();
            _ninjectKernel.Bind<IProductDescription>().To<EFProductDescription>();
        }

    }
}