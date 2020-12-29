namespace TrendyMemes.Common.Attributes
{
    using System.ComponentModel.DataAnnotations;
    using System.Runtime.CompilerServices;

    using TrendyMemes.Common;

    public class MaxLengthWithCustomMessageAttribute : MaxLengthAttribute
    {
        public MaxLengthWithCustomMessageAttribute(int length, [CallerMemberName] string caller = null)
            : base(length)
        {
            this.ErrorMessage = ErrorMessages.LengthMax(length, caller);
        }
    }
}
