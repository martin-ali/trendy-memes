namespace TrendyMemes.Common.Attributes
{
    using System.ComponentModel.DataAnnotations;
    using System.Runtime.CompilerServices;

    using TrendyMemes.Common;

    public class MinLengthWithCustomMessageAttribute : MinLengthAttribute
    {
        public MinLengthWithCustomMessageAttribute(int length, [CallerMemberName] string caller = null)
            : base(length)
        {
            this.ErrorMessage = ErrorMessages.LengthMin(length, caller);
        }
    }
}
