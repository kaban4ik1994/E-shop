using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Domain.Abstract;
using Domain.Concrete;
using Domain.Entities;
using Moq;
using Ninject;


namespace WebUI.Infrastructure
{
    public class NinjectControllerFactory : DefaultControllerFactory
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
            var mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products)
                .Returns(new List<Product>
                {
                    new Product {ProductID = 1,Name = "p1", Price = 20, Category = "cat1" }, 
                    new Product {ProductID = 2, Name = "p2", Price =25 , Category = "cat2" },
                    new Product {ProductID = 3, Name = "p3", Price = 30, Category = "cat3" },
                    new Product {ProductID = 4, Name = "p4", Price = 35, Category = "cat1"},
                    new Product {ProductID = 5, Name = "p5", Price = 40, Category = "cat5" }
                }.AsQueryable);
            _ninjectKernel.Bind<IProductRepository>().ToConstant(mock.Object);
            //  _ninjectKernel.Bind<IProductRepository>().To<EfProductRepository>();
        }
    }
}