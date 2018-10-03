using ScribeLibrary;
using ScribeTests.TestObjects;
using System.Collections.Generic;
using System.IO;
using Xunit;
namespace ScribeTests
{
    public class Scribe_WriteTests
    {
        [Fact]
        public void Scribe_Write_Class_Writes_Properties_To_String_Value()
        {
            var expected = "12345000000000000000BusinessName        5555555555          4567890000000000000000000000000000010000";
            var business = new SimpleBusiness()
            {
                BusinessId = 12345,
                BusinessName = "BusinessName",
                BusinessTelephoneNumber = "5555555555",
                TaxId = "456789",
                CashOnHand = "10000"                       
            };

            using (var writer = new StringWriter())
            {
                IScribe scribe = new Scribe(writer);
                scribe.Write(business);
                var actual = writer.ToString();

                Assert.Equal(expected, actual);
            }                       
        }

        [Fact]
        public void Scribe_Write_Class_With_One_Property_Without_Attribute_Writes_Values_With_Attributes_Only()
        {
            var expected = "12345000000000000000BusinessName        5555555555          45678900000000000000";

            var business = new BusinessWithOneFieldWithoutAttribute()
            {
                BusinessId = 12345,
                BusinessName = "BusinessName",
                BusinessTelephoneNumber = "5555555555",
                TaxId = "456789",
                Something = "something"                
            };

            using (var writer = new StringWriter())
            {
                IScribe scribe = new Scribe(writer);
                scribe.Write(business);
                var actual = writer.ToString();

                Assert.Equal(expected, actual);
                Assert.DoesNotContain(actual, business.Something);
            }
        }

        [Fact]
        public void Scribe_Write_List_Of_Classes_With_Attributes_Writes_Values()
        {
            var expected = "54321     John           Smith          8765309   Jenny          Smith          ";

            var merchantA = new Merchant() { MerchantFirstName = "John", MerchantLastName = "Smith", MerchantId = "54321" };
            var merchantB = new Merchant() { MerchantFirstName = "Jenny", MerchantLastName = "Smith", MerchantId = "8765309" };
            IList<Merchant> merchants = new List<Merchant>() { merchantA, merchantB };           

            using (var writer = new StringWriter())
            {
                IScribe scribe = new Scribe(writer);
                scribe.Write(merchants);
                
                var actual = writer.ToString();

                Assert.Equal(expected, actual);
            }
        }
    }
}