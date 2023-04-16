using AuthorizeNet.Api.Contracts.V1;
using AuthorizeNet.Api.Controllers;
using AuthorizeNet.Api.Controllers.Bases;
using Microsoft.Extensions.Configuration;
using ProAirApiServices.DataLayer.Core.PaymentEngine.Models;

namespace ProAirApiServices.DataLayer.Core.PaymentEngine.Gateways
{
    public abstract class AuthorizeNetProcessor
    {
        #region Constants

        private const string CURRENCY_CODE = "USD";
        
        #endregion

        #region Fields

        private readonly IConfiguration config;
        private createTransactionController? _transactionController;

        #endregion

        public AuthorizeNetProcessor(IConfiguration config)
        {
            this.config = config;
        }

        public ANetApiResponse? ChargeCreditCard(CheckoutDto checkoutModel)
        {
            ApiOperationBase<ANetApiRequest,ANetApiResponse>.RunEnvironment = GetGatewayEnvironment();
            ApiOperationBase<ANetApiRequest, ANetApiResponse>.MerchantAuthentication = new merchantAuthenticationType()
            {
                name = config["AuthorizeNet:ApiLoginId"],
                ItemElementName = ItemChoiceType.transactionKey,
                Item = config["AuthorizeNet:TransactionKey"],
            };

            var request = new createTransactionRequest { transactionRequest = new transactionRequestType
                {
                    transactionType = transactionTypeEnum.authCaptureTransaction.ToString(),              
                    currencyCode = CURRENCY_CODE,
                    amount = checkoutModel.Amount,
                    payment = checkoutModel.PaymentType,
                    billTo = checkoutModel.CustomerAddressType,
                    transactionSettings = new settingType[] { new settingType { settingName = "testRequest",settingValue="false"} }
                }
            };

            _transactionController = new createTransactionController(request);
            _transactionController?.Execute();

            var response = _transactionController?.GetApiResponse();

            return response;
        }

        public AuthorizeNet.Environment? GetGatewayEnvironment()
        {            
            switch (config["AuthorizeNet:Environment"])
            {
                case "sandbox":
                    return AuthorizeNet.Environment.SANDBOX;
                case "production":
                    return AuthorizeNet.Environment.PRODUCTION;
            }            

            return null;
        }
    }
}
