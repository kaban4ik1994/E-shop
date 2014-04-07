using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Domain.Abstract;
using Domain.Entities;

namespace Domain.Concrete
{

    public class EmailSettings
    {
        public string MailToAddress = "orders@shop.com";
        public string MailFromAddress = "eshop@shop.com";
        public bool UseSsl = true;
        public string UserName = "UserName";
        public string Password = "Password";
        public string ServerName = "smpt.shop.com";
        public int ServerPort = 587;
        public bool WriteAsFile = false;
        public string FileLocation = @"c:\shop";

    }

    public class EmailOrderProcessor : IOrderProcessor
    {
        private EmailSettings _emailSettings;

        public EmailOrderProcessor(EmailSettings settings)
        {
            _emailSettings = settings;
        }

        public void ProcessOrder(Cart cart, ShippingDetails shippingDetails) //нужно выставить права доступа, чтобы работало
        {
            using (var smptClient = new SmtpClient())
            {
                smptClient.EnableSsl = _emailSettings.UseSsl;
                smptClient.Host = _emailSettings.ServerName;
                smptClient.Port = _emailSettings.ServerPort;
                smptClient.UseDefaultCredentials = false;
                smptClient.Credentials = new NetworkCredential(_emailSettings.UserName, _emailSettings.Password);

                if (_emailSettings.WriteAsFile)
                {
                    smptClient.DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory;
                    smptClient.PickupDirectoryLocation = _emailSettings.FileLocation;
                    smptClient.EnableSsl = false;
                }

                var body = new StringBuilder().AppendLine("A new order has been submitted").AppendLine("---").AppendLine("Items:");
                foreach (var line in cart.Lines)
                {
                    var subtotal = line.Product.ListPrice*line.Quantity;
                    body.AppendFormat("{0} x {1} (subtotal: {2:C}", line.Quantity, line.Product.Name, subtotal);
                }

                body.AppendFormat("Total order value: {0:C}", cart.ComputeTotalValue())
                    .AppendLine("----")
                    .AppendLine("Ship to:")
                    .AppendLine(shippingDetails.Name)
                    .AppendLine(shippingDetails.Line1)
                    .AppendLine(shippingDetails.Line2 ?? "")
                    .AppendLine(shippingDetails.Line3 ?? "")
                    .AppendLine(shippingDetails.City)
                    .AppendLine(shippingDetails.City)
                    .AppendLine(shippingDetails.Zip)
                    .AppendLine("-----");
                var mailMessage = new MailMessage(_emailSettings.MailFromAddress, _emailSettings.MailToAddress,
                    "New order", body.ToString());
                if (_emailSettings.WriteAsFile)
                {
                    mailMessage.BodyEncoding = Encoding.ASCII;
                }

                smptClient.Send(mailMessage);
            }
        }


    }
}
