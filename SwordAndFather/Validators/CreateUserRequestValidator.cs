using SwordAndFather.Models;

namespace SwordAndFather.Validators
{
    public class CreateUserRequestValidator
    {
        public bool Validate(CreateUserRequest requestToValidate)
        {
            return (string.IsNullOrEmpty(requestToValidate.Username)
                   || string.IsNullOrEmpty(requestToValidate.Password));
        }
    }
}

//becomes an attribute when it's wrapped in square brackets. attributes are classes too. just like controllers, all attribute classes end with attribute, though you don't have to see it within the []. it's not necessary. 
//[HttpPost("register")]the metadata we expect
//everytime api.users it hits this method