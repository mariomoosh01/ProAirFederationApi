using ProAirApiServices.DataLayer.Models.Dto.Login;
using ProAirApiServices.DataLayer.Models.Dto.Members;

namespace ProAirApiServices.DataLayer.DataServices.Contracts
{
    public interface IMemberServices
    {
        bool AuthenticateMember(MemberDto model);        
        string GetDisplayName(string displayName);
        MemberDto SaveMember(MemberDto model);
        MembersCreditCardsDto SaveMemberCreditCard(MembersCreditCardsDto model);
        MemberDto GetMemberProfile(string email);
    }
}
