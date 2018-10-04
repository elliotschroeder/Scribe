using ScribeLibrary;
using System.Collections.Generic;

namespace ScribeTests.TestObjects
{
    public abstract class Business
    {
        [FixedLengthField(0, 20, Alignment.Left, Padding.Zeros)]
        public int BusinessId { get; set; }

        [FixedLengthField(1, 20, Alignment.Left, Padding.Spaces)]
        public string BusinessName { get; set; }

        [FixedLengthField(2, 20, Alignment.Left, Padding.Spaces)]
        public string BusinessTelephoneNumber { get; set; }

        [FixedLengthField(3, 20, Alignment.Left, Padding.Zeros)]
        public string TaxId { get; set; }
    }

    public class SimpleBusiness : Business
    {
        [FixedLengthField(4, 20, Alignment.Right, Padding.Zeros)]
        public string CashOnHand { get; set; }
    }

    [FixedLengthFile(20)]
    public class SimpleBusinessWithFixedLengthFileAttribute : Business
    {
        [FixedLengthField(4, 20, Alignment.Right, Padding.Zeros)]
        public string CashOnHand { get; set; }
    }

    public class ComplexBusiness : Business
    {
        public List<Merchant> Merchants { get; set; }

    }

    public class BusinessWithOneFieldWithoutAttribute : Business
    {
        public string Something { get; set; }
    }

    public class BusinessWithPrivateProperty : Business
    {
        private int PrivateInteger { get; set; }
    }

    public class Merchant
    {
        [FixedLengthField(0, 10, Alignment.Left, Padding.Spaces)]
        public string MerchantId { get; set; }

        [FixedLengthField(1, 15, Alignment.Left, Padding.Spaces)]
        public string MerchantFirstName { get; set; }

        [FixedLengthField(2, 15, Alignment.Left, Padding.Spaces)]
        public string MerchantLastName { get; set; }
    }

    public class BusinessWithPropertyThatUsesCustomPadding
    {
        [FixedLengthField(0, 10, Alignment.Left, Padding.Custom, 'X')]
        public string BusinessName { get; set; }
     
    }

    public class BusinessWithPropertyThatUsesCustomerPaddingNoPaddingChar
    {
        [FixedLengthField(0, 5, Alignment.Left, Padding.Custom)]
        public string BusinessDBA { get; set; }
    }
}
