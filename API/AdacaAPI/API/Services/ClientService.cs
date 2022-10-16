using Adaca.Api.DataModels;
using Adaca.Api.DTO;
using Adaca.Api.Infrastructure.Enum;
using Adaca.Api.Infrastructure.Extensions;
using Adaca.Api.Interfaces;
using Adaca.Api.Models;
using Adaca.Api.Util;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Adaca.Api.Services
{
    public class ClientService : BaseService<ClientService>, IClientService
    {
        private readonly IClientUnitOfWork _clientUnitOfWork;
        private readonly IConfiguration _configuration;
        private readonly int MinLoanAmt;
        private readonly int MaxLoanAmt;
        private readonly int MinTimeTrading;
        private readonly int MaxTimeTrading;
        public ClientService(IBaseDependenciesService<ClientService> dependencies,
            IConfiguration configuration,
        IClientUnitOfWork clientUnitOfWork) : base(dependencies)
        {
            _clientUnitOfWork = clientUnitOfWork;
            _configuration = configuration;
            MinLoanAmt = _configuration.GetValue<int>(Constants.MinLoanAmount);
            MaxLoanAmt = _configuration.GetValue<int>(Constants.MaximuLoanAmount);
            MinTimeTrading = _configuration.GetValue<int>(Constants.MinTimeTrading);
            MaxTimeTrading = _configuration.GetValue<int>(Constants.MaxTimeTrading);
        }

        public async Task<ClientResponse> SubmitLoan(InsertClientDTO dtoModel)
        {
            var result =await ValidateClientData(dtoModel);
            if (result.Decision != EnumDecisionType.Qualified.ToString())
                return result;

            var mappedClient = Mapper.Map<Client>(dtoModel);
            await  _clientUnitOfWork.clientRepository.Add(mappedClient);
            _clientUnitOfWork.Save();

            return result;
        }

        private async Task<ClientResponse> ValidateClientData(InsertClientDTO dtoModel)
        {
            ClientResponse response = new ClientResponse();
            List<ValidationResult> validateResultList = new List<ValidationResult>();
           
           
            if(!(dtoModel.FirstName.HasValue() && dtoModel.LastName.HasValue()))
            {
                var validate = new ValidationResult();
                validate.Rule = "NameRule";
                validate.Message = "Either FirstName or LastName must have value";
                validate.Decision = EnumDecisionType.Unknown.ToString();
                validateResultList.Add(validate);
            }
            if (!(dtoModel.EmailAddress.HasValue() && dtoModel.PhoneNumber.HasValue()))
            {
                var validate = new ValidationResult();
                validate.Rule = "EmailAddressAndPhoneNumber";
                validate.Message = "Either EmailAddress or PhoneNumber must have value";
                validate.Decision = EnumDecisionType.Unknown.ToString();
                validateResultList.Add(validate);
            }

            if ((dtoModel.EmailAddress.HasValue() && !dtoModel.EmailAddress.IsValidEmailAddress()))
            {
                var validate = new ValidationResult();
                validate.Rule = "EmailAddress";
                validate.Message = "Invalid Email Address format";
                validate.Decision = EnumDecisionType.Unknown.ToString();
                validateResultList.Add(validate);
            }

           // var regexPhone = new Regex(@"^(\+?\(61\)|\(\+?61\)|\+?61|\(0[1-9]\)|0[1-9])?( ?-?[0-9]){7,9}$");
            if (dtoModel.PhoneNumber.HasValue() && !(dtoModel.PhoneNumber.IsValidPhoneNumber())) {
                var validate = new ValidationResult();
                validate.Rule = "PhoneNumber";
                validate.Message = "Invalid Phone number forma.Phone number  must start with 04 or +614 followed by 8 digits. Landline number must start with 02, 03, 07 or 08 followed by 8 digits";
                validate.Decision = EnumDecisionType.Unknown.ToString();
                validateResultList.Add(validate);
            }

           // var regexABN = new Regex(@"^(\d *?){11}$");
            if (dtoModel.BusinessNumber.HasValue() && !(dtoModel.BusinessNumber.IsValidBusinessNumberFormat()))
            {
                var validate = new ValidationResult();
                validate.Rule = "BusinessNumberRule";
                validate.Message = "Incorrect Business Number";
                validate.Decision = EnumDecisionType.Unqualified.ToString();
                validateResultList.Add(validate);
            }


            if (!(dtoModel.LoanAmount   > MinLoanAmt && dtoModel.LoanAmount < MaxLoanAmt)) {
                var validate = new ValidationResult();
                validate.Rule = "LoanAmount";
                validate.Message = "Loan Amount is invalid.Must be above $10.000 and below $100.000";
                validate.Decision = EnumDecisionType.Unknown.ToString();
                validateResultList.Add(validate);
            }

            if (!(dtoModel.CitizenshipStatus.ToLower() == Constants.Citizen.ToLower() || dtoModel.CitizenshipStatus.ToLower() == Constants.PermanentResident.ToLower()))
            {
                var validate = new ValidationResult();
                validate.Rule = "CitizenshipStatus";
                validate.Message = "CitizenshipStatus is invalid.Can be a 'Citizen' or 'Permanent Resident'";
                validate.Decision = EnumDecisionType.Unknown.ToString();
                validateResultList.Add(validate);
            }

            if (!(dtoModel.TimeTrading > MinTimeTrading && dtoModel.TimeTrading < MaxTimeTrading))
            {
                var validate = new ValidationResult();
                validate.Rule = "TimeTrading";
                validate.Message = "TimeTrading is invalid.CAN be a number and greater than 1 and less than 20";
                validate.Decision = EnumDecisionType.Unknown.ToString();
                validateResultList.Add(validate);
            }

            if (!(dtoModel.CountryCode.ToUpper() == Constants.CountryCode.ToUpper()))
            {
                var validate = new ValidationResult();
                validate.Rule = "CountryCode";
                validate.Message = "CountryCode is invalid.Must be AU";
                validate.Decision = EnumDecisionType.Unknown.ToString();
                validateResultList.Add(validate);
            }
            if(!dtoModel.Industry.HasValue())
            {
                var validate = new ValidationResult();
                validate.Rule = "Industry";
                validate.Message = "Industry must have value";
                validate.Decision = EnumDecisionType.Unknown.ToString();
                validateResultList.Add(validate);
            }
            else
            {
                string decisionType = CommonFuncs.IsValidStringContentExist(dtoModel.Industry, "Banned") ? EnumDecisionType.Unqualified.ToString() : CommonFuncs.IsValidStringContentExist(dtoModel.Industry, "Industry") ? EnumDecisionType.Qualified.ToString() : EnumDecisionType.Unknown.ToString();
                var validate = new ValidationResult();
                validate.Rule = "Industry";
                validate.Message = "Invalid Industry";
                validate.Decision = decisionType;
                validateResultList.Add(validate);
            }

            response.Decision = validateResultList.Where(x => x.Decision == EnumDecisionType.Unqualified.ToString()).Count() > 0 ?
                                                                        EnumDecisionType.Unqualified.ToString() :
                                                                            validateResultList.Where(x => x.Decision == EnumDecisionType.Unknown.ToString()).Count() > 0 ?
                                                                                EnumDecisionType.Unknown.ToString() : EnumDecisionType.Qualified.ToString();
            response.ValidationResult = validateResultList.Where(x => x.Message.HasValue()).ToList(); ;

            return response;
        }

        public async Task<List<ClientListResponse>> GetClientList(SearchClientDTO dtoModel)
        {
            var result = await _clientUnitOfWork.clientRepository.RetrieveClient();
            if (result == null || result.Count == 0)
                return new List<ClientListResponse>();


            if (dtoModel.SearchKeyword.HasValue())
            {
                result = result.Where(
                        x => x.EmailAddress.Equals(dtoModel.SearchKeyword) ||
                        x.PhoneNumber.Equals(dtoModel.SearchKeyword) ||
                        x.BusinessNumber.Equals(dtoModel.SearchKeyword) ||
                        x.Industry.Equals(dtoModel.SearchKeyword)
                    ).ToList();
            }

            if (dtoModel.OrderClientField != null)
            {
                var fieldName = dtoModel.OrderClientField.FieldName;
                var orderBy = dtoModel.OrderClientField.OrderBy;
                if (fieldName.HasValue)
                {
                    if (fieldName == (int)EnumFilterAndOrderByFields.Citizenship)
                    {
                        if (orderBy.HasValue)
                        {
                            if (orderBy == (int)EnumOrderByType.ASC)
                                result = result.OrderBy(x => x.CitizenshipStatus).ToList();
                            else
                                result = result.OrderByDescending(x => x.CitizenshipStatus).ToList();
                        }
                    }

                    if (fieldName == (int)EnumFilterAndOrderByFields.LoanAmount)
                    {
                        if (orderBy.HasValue)
                        {
                            if (orderBy == (int)EnumOrderByType.ASC)
                                result = result.OrderBy(x => x.LoanAmount).ToList();
                            else
                                result = result.OrderByDescending(x => x.LoanAmount).ToList();
                        }
                    }

                    if (fieldName == (int)EnumFilterAndOrderByFields.TimeTrading)
                    {
                        if (orderBy.HasValue)
                        {
                            if (orderBy == (int)EnumOrderByType.ASC)
                                result = result.OrderBy(x => x.TimeTrading).ToList();
                            else
                                result = result.OrderByDescending(x => x.TimeTrading).ToList();
                        }
                    }
                    if (fieldName == (int)EnumFilterAndOrderByFields.EmailAddress)
                    {
                        if (orderBy.HasValue)
                        {
                            if (orderBy == (int)EnumOrderByType.ASC)
                                result = result.OrderBy(x => x.EmailAddress).ToList();
                            else
                                result = result.OrderByDescending(x => x.EmailAddress).ToList();
                        }
                    }
                }
            }


            if (dtoModel.FilterClientField != null)
            {
                var fieldName = dtoModel.FilterClientField.FieldName;
                var filterKeyword = dtoModel.FilterClientField.FilterKeyword;
                if (fieldName.HasValue)
                {
                    if (fieldName == (int)EnumFilterAndOrderByFields.Citizenship)
                    {
                        if (filterKeyword.HasValue())
                            result = result.Where(x => x.CitizenshipStatus == filterKeyword).ToList();

                    }

                    if (fieldName == (int)EnumFilterAndOrderByFields.LoanAmount)
                    {
                        if (filterKeyword.HasValue())
                            result = result.Where(x => x.LoanAmount == Convert.ToInt32(filterKeyword)).ToList();
                    }

                    if (fieldName == (int)EnumFilterAndOrderByFields.TimeTrading)
                    {
                        if (filterKeyword.HasValue())
                            result = result.Where(x => x.TimeTrading == Convert.ToInt32(filterKeyword)).ToList();
                    }
                    if (fieldName == (int)EnumFilterAndOrderByFields.EmailAddress)
                    {
                        if (filterKeyword.HasValue())
                            result = result.Where(x => x.EmailAddress ==filterKeyword).ToList();
                    }
                }
            }


            var mappedClient = Mapper.Map<List<ClientListResponse>>(result);
            return mappedClient;
        }
        
    }
}
