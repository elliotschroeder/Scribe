using ScribeLibrary;
using ScribeTests.TestObjects;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            var fieldFormatter = new FieldFormatter(); 
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
                IScribe scribe = new Scribe(writer, fieldFormatter);
                scribe.Write(business);
                var actual = writer.ToString();

                Assert.Equal(expected, actual);
            }                       
        }

        [Fact]
        public void Scribe_Write_Class_With_One_Property_Without_Attribute_Writes_Values_With_Attributes_Only()
        {
            var expected = "12345000000000000000BusinessName        5555555555          45678900000000000000";

            var fieldFormatter = new FieldFormatter();
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
                IScribe scribe = new Scribe(writer, fieldFormatter);
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

            var fieldFormatter = new FieldFormatter();
            var merchantA = new Merchant() { MerchantFirstName = "John", MerchantLastName = "Smith", MerchantId = "54321" };
            var merchantB = new Merchant() { MerchantFirstName = "Jenny", MerchantLastName = "Smith", MerchantId = "8765309" };
            IList<Merchant> merchants = new List<Merchant>() { merchantA, merchantB };           

            using (var writer = new StringWriter())
            {
                IScribe scribe = new Scribe(writer, fieldFormatter);
                scribe.Write(merchants);
                
                var actual = writer.ToString();

                Assert.Equal(expected, actual);
            }
        }

        [Fact]
        public void Scribe_Write_NULL_Values_On_Properties_Resolves_To_Empty_String()
        {
            var expected = string.Empty.PadLeft(80);

            var fieldFormatter = new FieldFormatter();
            var merchantA = new Merchant() { MerchantFirstName = null, MerchantLastName = null, MerchantId = null };
            var merchantB = new Merchant() { MerchantFirstName = null, MerchantLastName = null, MerchantId = null };
            IList<Merchant> merchants = new List<Merchant>() { merchantA, merchantB };

            using (var writer = new StringWriter())
            {
                IScribe scribe = new Scribe(writer, fieldFormatter);
                scribe.Write(merchants);

                var actual = writer.ToString();

                Assert.Equal(expected, actual);
            }
        }

        [Fact]
        public void Scribe_Write_Only_Writes_Public_Properties_Of_Classes()
        {
            var expected = "12345000000000000000BusinessName        5555555555          45678900000000000000";

            var fieldFormatter = new FieldFormatter();
            var business = new BusinessWithPrivateProperty()
            {
                BusinessId = 12345,
                BusinessName = "BusinessName",
                BusinessTelephoneNumber = "5555555555",
                TaxId = "456789",
            };

            using (var writer = new StringWriter())
            {
                IScribe scribe = new Scribe(writer, fieldFormatter);
                scribe.Write(business);
                var actual = writer.ToString();

                Assert.Equal(expected, actual);
            }
        }

        [Fact]
        public void Scribe_Write_Uses_FixedLengthFileAttribute_To_Add_Line_Endings()
        {
            var expected = "12345000000000000000"
                + Environment.NewLine
                + "BusinessName        "
                + Environment.NewLine
                + "5555555555          "
                + Environment.NewLine
                + "45678900000000000000"
                + Environment.NewLine
                + "00000000000000010000"
                + Environment.NewLine;

            var fieldFormatter = new FieldFormatter();
            var business = new SimpleBusinessWithFixedLengthFileAttribute()
            {
                BusinessId = 12345,
                BusinessName = "BusinessName",
                BusinessTelephoneNumber = "5555555555",
                TaxId = "456789",
                CashOnHand = "10000"
            };

            using (var writer = new StringWriter())
            {
                IScribe scribe = new Scribe(writer, fieldFormatter);
                scribe.Write(business);
                var actual = writer.ToString();

                Assert.Equal(expected, actual);
            }
        }

        [Fact]
        public void Scibe_Write_Property_With_Custom_Char_Writes()
        {
            var expected = "ZZZZZXXXXX";

            var fieldFormatter = new FieldFormatter();
            var business = new BusinessWithPropertyThatUsesCustomPadding() { BusinessName = "ZZZZZ" };

            using (var writer = new StringWriter())
            {
                IScribe scribe = new Scribe(writer, fieldFormatter);
                scribe.Write(business);
                var actual = writer.ToString();

                Assert.Equal(expected, actual);
            }
        }

        [Fact]
        public void Scibe_Write_Property_With_Custom_Padding_No_Char_Supplied_Writes_Values()
        {
            var expected = "ZZZ  ";

            var fieldFormatter = new FieldFormatter();
            var business = new BusinessWithPropertyThatUsesCustomerPaddingNoPaddingChar() { BusinessDBA = "ZZZ" };

            using (var writer = new StringWriter())
            {
                IScribe scribe = new Scribe(writer, fieldFormatter);
                scribe.Write(business);
                var actual = writer.ToString();

                Assert.Equal(expected, actual);
            }
        }

        [Fact(Skip ="Performance Test")]
        public void Scribe_Write_Performance_Test()
        {
            var business = new SimpleBusiness()
            {
                BusinessId = 12345,
                BusinessName = "BusinessName",
                BusinessTelephoneNumber = "5555555555",
                TaxId = "456789",
                CashOnHand = "10000"
            };

            var businessList = new List<SimpleBusiness>();

            for (int i = 0; i < 1000000000; i++)
            {
                businessList.Add(business);
            }

            using (var writer = new StringWriter())
            {
                var stopwatch = new Stopwatch();

                IFieldFormatter fieldFormatter = new FieldFormatter();
                IScribe scribe = new Scribe(writer, fieldFormatter);

                stopwatch.Start();
                scribe.Write(businessList);
                stopwatch.Stop();

                var timeToRun = stopwatch.Elapsed.TotalSeconds;

                Assert.True(timeToRun <= 15);

            }

        }
    }
}