using System;
using System.Collections.Generic;
using System.Text;

namespace BarcodeScanner.ObjectClasses
{
    public class Barcode
    {
#pragma warning disable IDE1006 // Naming Styles
        String codeValue { get; set; } = String.Empty;

        String codeType { get; set; } = String.Empty;
#pragma warning restore IDE1006 // Naming Styles

        public String BarcodeValue 
        {
            get => codeValue;
            set => codeValue = value;
        }

        public String BarcodeType
        {
            get => codeType;
            set => codeType = value;
        }
    }
}
