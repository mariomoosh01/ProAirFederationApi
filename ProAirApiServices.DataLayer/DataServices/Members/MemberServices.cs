using AutoMapper;
using ProAirApiServices.DataLayer.Core.Mapper;
using ProAirApiServices.DataLayer.Core.Security;
using ProAirApiServices.DataLayer.DataAccess.Core;
using ProAirApiServices.DataLayer.DataAccess.Entities;
using ProAirApiServices.DataLayer.DataAccess.Repositories;
using ProAirApiServices.DataLayer.DataServices.Contracts;
using ProAirApiServices.DataLayer.Models.Dto.Login;
using ProAirApiServices.DataLayer.Models.Dto.Members;
using Serilog;

namespace ProAirApiServices.DataLayer.DataServices.MemberServices
{
    public class MemberServices: IMemberServices
    {
        private readonly IRepository<Members> _membersRepo;
        private readonly IRepository<MembersCreditCards> _membersCreditCardsRepo;
        private readonly IMapper _mapper = ServicesMapper.Mapper;
        private readonly TextEncryptorEngine encryptor;

        public MemberServices(ProAirDbContext dbContext, TextEncryptorEngine encryptor)
        {            
            _membersRepo = new Repository<Members>(dbContext);
            _membersCreditCardsRepo = new Repository<MembersCreditCards>(dbContext);
            this.encryptor = encryptor;
        }

        public bool AuthenticateMember(MemberDto model)
        {
            var user = GetMemberProfile(model.Email);

            if (user == null) return false;

            var hashPwd = Convert.FromBase64String(user.Password);                        

            return encryptor.VerifyHash(model.Password, hashPwd);
        }

        public MemberDto GetMemberProfile(string email)
        {
            var profile = _membersRepo.FirstOrDefault(w => w.Email == email);

            return _mapper.Map<MemberDto>(profile);
        }

        public string GetDisplayName(string displayName)
        {
            Log.Information("Getting display name");
            var dp = _membersRepo.FirstOrDefault(w => w.DisplayName == displayName);

            return dp?.DisplayName??"";
        }

        public MemberDto SaveMember(MemberDto model)
        {
            var entity = _mapper.Map<Members>(model);
            var insert = _membersRepo.Add(entity);

            return _mapper.Map<MemberDto>(insert);
        }

        public MembersCreditCardsDto SaveMemberCreditCard(MembersCreditCardsDto model)
        {
            var entity = _mapper.Map<MembersCreditCards>(model);
            var insert = _membersCreditCardsRepo.Add(entity);

            return _mapper.Map<MembersCreditCardsDto>(insert);

        }
    }
}
