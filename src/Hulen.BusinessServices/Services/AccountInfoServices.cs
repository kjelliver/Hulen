using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using Hulen.BusinessServices.Interfaces;
using Hulen.Storage.DTO;
using Hulen.Storage.Interfaces;
using Hulen.Storage.Repositories;

namespace Hulen.BusinessServices.Services
{
    public class AccountInfoServices : IAccountInfoServices
    {
        private readonly IAccountInfoRepository _repository = new AccountInfoRepository();
        //private readonly Application _application = new Application();


        public ICollection<AccountInfoDTO> GetAllAccounts()
        {
            return _repository.GetAllAccountCategories(); 
        }

        public void StoreNewAccountInfo(AccountInfoDTO account)
        {
            _repository.Add(account);
        }

        public AccountInfoDTO GetAccountById(Guid id)
        {
            return _repository.GetById(id);
        }

        public void UpdateAccountInfo(AccountInfoDTO accountInfo)
        {
            _repository.Update(accountInfo);
        }

        public void Delete(AccountInfoDTO  accountInfo)
        {
            _repository.Delete(accountInfo);
        }

        public void GeneratePdf()
        {
            //GeneratePDF

            //_accountInfoReporting.GeneratePdf();

            //MemoryStream ms = new MemoryStream();



            //byte[] byteInfo = pdf.Output();
            //ms.Write(byteInfo, 0, byteInfo.Length);
            //ms.Position = 0;

            //return ms;
        }
    }
}
