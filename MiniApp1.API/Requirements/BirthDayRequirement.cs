using Microsoft.AspNetCore.Authorization;

namespace MiniApp1.API.Requirements
{
    public class BirthDayRequirement : IAuthorizationRequirement
    {
        public int RequirementAge { get; set; }

        public BirthDayRequirement(int requirementAge)
        {
            RequirementAge = requirementAge;
        }

        /// <summary>
        /// Kullanıcının doğum gününe göre yetkilendirme yapar
        /// </summary>
        public class BirthDayRequirementHandler : AuthorizationHandler<BirthDayRequirement>
        {
            protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
                BirthDayRequirement requirement)
            {
                var birhDate = context.User.FindFirst("birth-date");

                if (birhDate == null)
                {
                    context.Fail();
                    return Task.CompletedTask;
                }

                var today = DateTime.Now;

                var age = today.Year - Convert.ToDateTime(birhDate.Value).Year;

                //kullanıcının yaşı zorunlu yaştan büyük yada eşitse işlemi gerçekleştir
                if (age >= requirement.RequirementAge)
                {
                    context.Succeed(requirement);
                }
                else
                {
                    context.Fail();
                }

                return Task.CompletedTask;
            }
        }
    }
}