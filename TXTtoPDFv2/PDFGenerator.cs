using PdfFileWriter;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TXTtoPDFv2
{
    public class PDFGenerator
    {
        #region Enums
        /// <summary>
        /// the document Type enum(document orientation enum)
        /// </summary>
        public enum DocumentType
        {
            Portrait = 0,
            Landscape = 1
        }
        /// <summary>
        /// the document font enum => used for containing all prepared fonts for ease of access and understanding
        /// </summary>
        public enum DocumentFont
        {
            /// <summary>
            /// Arial Font
            /// </summary>
            ArialNormal = 0,
            /// <summary>
            /// Arial Font Bold
            /// </summary>
            ArialBold,
            /// <summary>
            /// Arial Font Italic
            /// </summary>
            ArialItalic,
            /// <summary>
            /// Arial Font Bold & Italic
            /// </summary>
            ArialBoldItalic,
            /// <summary>
            /// Times New Roman Font
            /// </summary>
            TimesNormal,
            /// <summary>
            /// Times New Roman Bold Font
            /// </summary>
            TimesBold,
            /// <summary>
            /// Times New Roman Italic Font
            /// </summary>
            TimesItalic,
            /// <summary>
            /// Times New Roman Bold & Italic Font
            /// </summary>
            TimesBoldItalic,
            /// <summary>
            /// Lucida Console Font
            /// </summary>
            LucidaNormal,
            /// <summary>
            /// Comic Sans MS Font
            /// </summary>
            Comic,
            /// <summary>
            /// Wingdings Font
            /// </summary>
            Symbol,
            /// <summary>
            /// DCSLTT10 Font (Default font because most reports are based upon it)
            /// </summary>
            DCSLTT
        }
        #endregion
        #region Constants
        /// <summary>
        /// the Standard Page Width For Portrait Orientation
        /// </summary>
        private const double PortraitPageWidth = 8.5;
        /// <summary>
        /// the Standard Page Height For Portrait Orientation
        /// </summary>
        private const double PortraitPageHeight = 11.0;

        /// <summary>
        /// the Standard Page Width for Landscape Orientation
        /// </summary>
        private const double LandscapePageWidth = 11.0;
        /// <summary>
        /// the Standard Page Height for Landscape Orientation
        /// </summary>
        private const double LandscapePageHeight = 8.5;

        /// <summary>
        /// the predefined Margin for the form
        /// </summary>
        private static double Margin = 0.5;
        /// <summary>
        /// the predefined heading for the form
        /// </summary>
        private static double HeadingHeight = 0.5;

        /// <summary>
        /// the Area Width for the standard PDF Portrait Page
        /// </summary>
        private static double PortraitAreaWidth = PortraitPageWidth - Margin;
        /// <summary>
        /// the Area Height for the standard PDF Portrait Page
        /// </summary>
        private static double PortraitAreaHeight = PortraitPageHeight - HeadingHeight;

        /// <summary>
        /// the Area Width for the standard PDF Landscape Page
        /// </summary>
        private static double LandscapeAreaWidth = LandscapePageWidth - Margin;
        /// <summary>
        /// the Area Height for the standard PDF Landscape Page
        /// </summary>
        private static double LandscapeAreaHeight = LandscapePageHeight - HeadingHeight;

        private static double PosX = Margin;
        private static double PosY = HeadingHeight;
        #endregion
        #region PDF Items
        /// <summary>
        /// the pdf Document all the pdf classes are based upon (pages, contents, fonts, etc, etc, etc)
        /// </summary>
        private static PdfDocument pdfDocument;
        // the pdf Page used for sequential data Draw and Display
        private static PdfPage pdfPage;
        /// <summary>
        /// the pdf Contents for  creating a canvas and saving contents
        /// </summary>
        private static PdfContents pdfContents;
        //All pdf fonts will have their own objects
        #region PDF Fonts
        private static PdfFont ArialNormal;
        private static PdfFont ArialBold;
        private static PdfFont ArialItalic;
        private static PdfFont ArialBoldItalic;
        private static PdfFont TimesNormal;
        private static PdfFont TimesBold;
        private static PdfFont TimesItalic;
        private static PdfFont TimesBoldItalic;
        private static PdfFont LucidaNormal;
        private static PdfFont Comic;
        private static PdfFont Symbol;
        private static PdfFont DCSLTT;
        #endregion
        /// <summary>
        /// the Main Pdf font that will be initialized for the document to use
        /// </summary>
        private static PdfFont pdfFont;
        /// <summary>
        /// the initial base line for the text to be drawn upon
        /// </summary>
        private static double BaseLine = AreaHeight;
        #endregion
        // the in Use settings for the document / page
        #region Non Constant Page Settings
        /// <summary>
        /// the page Width
        /// </summary>
        private static double PageWidth { get; set; }
        /// <summary>
        /// the page height
        /// </summary>
        private static double PageHeight { get; set; }
        /// <summary>
        /// the area width for drawing the text
        /// </summary>
        private static double AreaWidth { get; set; }
        /// <summary>
        /// the area height for drawing the text
        /// </summary>
        private static double AreaHeight { get; set; }
        /// <summary>
        /// the process ID for the PDF
        /// Used for closing the Document later on
        /// </summary>
        public static Int32 ProcessID = -1;
        /// <summary>
        /// the Set page will initialize the needed seetings based on the document type
        /// </summary>
        /// <param name="documentType">the document type</param>
        private static void SetPage(DocumentType documentType)
        {
            switch (documentType)
            {
                case DocumentType.Portrait:
                    PageWidth   = PortraitPageWidth;
                    PageHeight  = PortraitPageHeight;
                    AreaWidth   = PortraitAreaWidth;
                    AreaHeight  = PortraitAreaHeight;
                    break;
                case DocumentType.Landscape:
                    PageWidth   = LandscapePageWidth;
                    PageHeight  = LandscapePageHeight;
                    AreaWidth   = LandscapeAreaWidth;
                    AreaHeight  = LandscapeAreaHeight;
                    break;
            }
        }
        #endregion
        /// <summary>
        /// the main file reader for the text document
        /// </summary>
        /// <param name="filePath">the path to the document</param>
        /// <returns>a list of the lines in the document</returns>
        private static List<String> FileReader(String filePath) { return System.IO.File.ReadAllLines(filePath).ToList(); }
        #region Initialization functions
        /// <summary>
        /// this function will initialize all the pdf fonts for later use
        /// Memory wise the deficit is minimal <= functionality wise it's optimal
        /// </summary>
        /// <param name="documentFont">unused parameter:</param>
        private static void InitializePDFFonts(DocumentFont documentFont)
        {
            //each font will be created with embeding link to Windows Fonts(Font not installed: Ops - Problem)
            ArialNormal = PdfFont.CreatePdfFont(pdfDocument, "Arial", FontStyle.Regular, true);
            ArialBold = PdfFont.CreatePdfFont(pdfDocument, "Arial", FontStyle.Bold, true);
            ArialItalic = PdfFont.CreatePdfFont(pdfDocument, "Arial", FontStyle.Italic, true);
            ArialBoldItalic = PdfFont.CreatePdfFont(pdfDocument, "Arial", FontStyle.Bold | FontStyle.Italic, true);
            TimesNormal = PdfFont.CreatePdfFont(pdfDocument, "Times New Roman", FontStyle.Regular, true);
            TimesBold = PdfFont.CreatePdfFont(pdfDocument, "Times New Roman", FontStyle.Bold, true);
            TimesItalic = PdfFont.CreatePdfFont(pdfDocument, "Times New Roman", FontStyle.Italic, true);
            TimesBoldItalic = PdfFont.CreatePdfFont(pdfDocument, "Times New Roman", FontStyle.Bold | FontStyle.Italic, true);
            LucidaNormal = PdfFont.CreatePdfFont(pdfDocument, "Lucida Console", FontStyle.Regular, true);
            Comic = PdfFont.CreatePdfFont(pdfDocument, "Comic Sans MS", FontStyle.Regular, true);
            Symbol = PdfFont.CreatePdfFont(pdfDocument, "Wingdings", FontStyle.Regular, true);
            DCSLTT = PdfFont.CreatePdfFont(pdfDocument, "dcsltt10", FontStyle.Regular, true);
        }
        /// <summary>
        /// the main initialization for the document that creates the base document:
        /// Empty Contains only page dimensions and orientation
        /// </summary>
        /// <param name="pageSettings">the pageSettings used for constructing the document</param>
        private static void InitializeDocument(PageSettings pageSettings)
        {
            pdfDocument = new PdfDocument(PaperType.A4, pageSettings.DocumentType == DocumentType.Landscape, UnitOfMeasure.Inch, pageSettings.FileName);
        }
        /// <summary>
        /// the initialization of the pdf font for use within the document
        /// </summary>
        /// <param name="documentFont">the document font to be atributed to the form</param>
        private static void InitializePdfFont(DocumentFont documentFont)
        {
            switch (documentFont)
            {
                case DocumentFont.ArialBold:
                    pdfFont = ArialBold;
                    return;
                case DocumentFont.ArialBoldItalic:
                    pdfFont = ArialBoldItalic;
                    return;
                case DocumentFont.ArialItalic:
                    pdfFont = ArialItalic;
                    return;
                case DocumentFont.ArialNormal:
                    pdfFont = ArialNormal;
                    return;
                case DocumentFont.Comic:
                    pdfFont = Comic;
                    return;
                case DocumentFont.LucidaNormal:
                    pdfFont = LucidaNormal;
                    return;
                case DocumentFont.Symbol:
                    pdfFont = Symbol;
                    return;
                case DocumentFont.TimesBold:
                    pdfFont = TimesBold;
                    return;
                case DocumentFont.TimesBoldItalic:
                    pdfFont = TimesBoldItalic;
                    return;
                case DocumentFont.TimesItalic:
                    pdfFont = TimesItalic;
                    return;
                case DocumentFont.TimesNormal:
                    pdfFont = TimesNormal;
                    return;
                case DocumentFont.DCSLTT:
                    pdfFont = DCSLTT;
                    return;
            }
        }
        /// <summary>
        /// the main initialization for the document settings: Margin and Heading Height
        /// The properties are based upon the font and the font size
        /// </summary>
        /// <param name="documentFont">the font of the curent document</param>
        /// <param name="FontSize">the font size</param>
        private static void InitializePageSettings(DocumentFont documentFont,Double FontSize)
        {
            switch (documentFont)
            {
                case DocumentFont.ArialBold:
                    Margin = 0.5;
                    HeadingHeight = ArialBold.LineSpacing(FontSize);
                    return;
                case DocumentFont.ArialBoldItalic:
                    Margin = 0.5;
                    HeadingHeight = ArialBoldItalic.LineSpacing(FontSize);
                    return;
                case DocumentFont.ArialItalic:
                    Margin = 0.5;
                    HeadingHeight = ArialItalic.LineSpacing(FontSize);
                    return;
                case DocumentFont.ArialNormal:
                    Margin = 0.5;
                    HeadingHeight = ArialItalic.LineSpacing(FontSize);
                    return;
                case DocumentFont.Comic:
                    Margin = 0.5;
                    HeadingHeight = Comic.LineSpacing(FontSize);
                    return;
                case DocumentFont.LucidaNormal:
                    Margin = 0.5;
                    HeadingHeight = LucidaNormal.LineSpacing(FontSize);
                    return;
                case DocumentFont.Symbol:
                    Margin = 0.5;
                    HeadingHeight = Symbol.LineSpacing(FontSize);
                    return;
                case DocumentFont.TimesBold:
                    Margin = 0.5;
                    HeadingHeight = TimesBold.LineSpacing(FontSize);
                    return;
                case DocumentFont.TimesBoldItalic:
                    Margin = 0.5;
                    HeadingHeight = TimesBoldItalic.LineSpacing(FontSize);
                    return;
                case DocumentFont.TimesItalic:
                    Margin = 0.5;
                    HeadingHeight = TimesItalic.LineSpacing(FontSize);
                    return;
                case DocumentFont.TimesNormal:
                    Margin = 0.5;
                    HeadingHeight = TimesNormal.LineSpacing(FontSize);
                    return;
                case DocumentFont.DCSLTT:
                    Margin = 0.5;
                    HeadingHeight = DCSLTT.LineSpacing(FontSize);
                    return;

            }

            PortraitAreaWidth = PortraitPageWidth - Margin;
            PortraitAreaHeight = PortraitPageHeight - HeadingHeight;

            LandscapeAreaWidth = LandscapePageWidth - Margin;
            LandscapeAreaHeight = LandscapePageHeight - HeadingHeight;

            PosX = Margin;
            PosY = HeadingHeight;
        }
        #endregion
        /// <summary>
        /// the function for the initialization of a new page and the equivalent contents
        /// </summary>
        private static void AddNewPage()
        {
            pdfPage = new PdfPage(pdfDocument);
            pdfContents = new PdfContents(pdfPage);
            BaseLine = AreaHeight;
        }
        /// <summary>
        /// the function for setting the base line for the text that follows to be drawn
        /// </summary>
        /// <param name="FontSize">the fontSize of the given line</param>
        /// <param name="pdfFont">the font of the given line</param>
        private static void SetBaseLine(Double FontSize,PdfFont pdfFont)
        {
            //we will always leave an empty line at the bottom of the page so that the printer will not print eroniously
            if (BaseLine < 2 * pdfFont.LineSpacing(FontSize)) AddNewPage();
            else BaseLine -= pdfFont.LineSpacing(FontSize);
        }
        /// <summary>
        /// the function for drawing a given text line
        /// </summary>
        /// <param name="line">the given line</param>
        /// <param name="FontSize">the text fontSize</param>
        private static void DrawText(String line, Double FontSize)
        {
            pdfContents.DrawText(pdfFont, FontSize, Margin, BaseLine, TextJustify.Left, line);
        }
        /// <summary>
        /// the central function for drawing of a text passed as a list of lines.
        /// </summary>
        /// <param name="text">the text to be drawn</param>
        /// <param name="FontSize">the font size for the text that needs to be drawn</param>
        private static void DrawAllText(List<String> text, Double FontSize)
        {
            //we always need to initiate the first page of the document
            AddNewPage();
            //then save the current graphic state for the document
            pdfContents.SaveGraphicsState();
            //we instantiate a line skip <= might be removed later on
            Boolean lineSkip = false;
            //we will parse through each line 
            foreach(String line in text)
            {
                //if line skip and the lenght of the line is 0 we skip the step
                if(lineSkip && line.Length == 0) continue;
                //we only reach this step if either lineSkip is false or the lenght is 0
                //if the lenght is 0 then the lineSkip is false
                if(line.Length == 0)
                {
                    //we set the line skip to true
                    lineSkip = true;
                    //then add a new page
                    AddNewPage();
                    //and skip the step
                    continue;
                }
                //if we reach this point then the line lenght is greater that 0
                //we then set the lineSkip to false if it is true
                if(lineSkip) lineSkip = false;
                //and the base functionality
                //we remove all special characters
                String textLine = new String(line.Select(x => x < ' ' || x > '~' && x < 160 || x > 0xffff ? ' ' : x).ToArray());
                //then draw the text 
                DrawText(textLine, FontSize);
                //and set the new base line
                SetBaseLine(FontSize, pdfFont);
            }
            //the new graphics state is saved to memory
            pdfContents.SaveGraphicsState();
            //then restore the graphics state to the pdf document
            pdfContents.RestoreGraphicsState();
            //and create the 
            pdfDocument.CreateFile();
        }
        /// <summary>
        /// the main procedure for generating a new PDF document
        /// </summary>
        /// <param name="textDocument">the text Document to be exported into a PDF</param>
        /// <param name="pageSettings">the page formatting settings for the document</param>
        public static void GenerateDocument(String textDocument, PageSettings pageSettings)
        {
            //the first step when creating a pdf document is initializing the document as all other objects are dependent on this one
            InitializeDocument(pageSettings);
            //then we initialize all the usable pdf fonts
            InitializePDFFonts(pageSettings.DocumentFont);
            //and then select the font to be used by the current document
            InitializePdfFont(pageSettings.DocumentFont);
            //and afterwards we initialize the page settings relative to the given Font and Font Size
            InitializePageSettings(pageSettings.DocumentFont, pageSettings.FontSize);
            //then set the Page Template
            SetPage(pageSettings.DocumentType);
            //read the document to be sent to the pdf
            List<String> DocumentText = FileReader(textDocument);
            //and draw all the text within it
            DrawAllText(DocumentText, pageSettings.FontSize);
            //and display the pdf
            var process = System.Diagnostics.Process.Start(pageSettings.FileName);
            //we also save the process ID so that I can close it on Keys.ESCAPE
            ProcessID = process.Id;
            //then so that the program doesn't just close we place a wait for the exit of the PDF
            process.WaitForExit();
        }
    }
    /// <summary>
    /// The PageSettings Class used for creating the necessary environment for the PDF
    /// </summary>
    public class PageSettings
    {
        #region Class Properties
        /// <summary>
        /// the fileName for the pdf generated by the program
        /// </summary>
        private String fileName = String.Empty;
        /// <summary>
        /// the document font for the pdf generated by the program
        /// </summary>
        private PDFGenerator.DocumentFont documentFont = PDFGenerator.DocumentFont.DCSLTT;
        /// <summary>
        /// the PDF fontSize for the document generated by the program
        /// </summary>
        private Double fontSize = 7.0;
        /// <summary>
        /// the documentType is actually the orientation: Sorry verry bad at naming shit
        /// </summary>
        private PDFGenerator.DocumentType documentType = PDFGenerator.DocumentType.Portrait;
        #endregion
        #region Accessors
        /// <summary>
        /// the main accessor for the fileName Property
        /// </summary>
        public String FileName
        {
            get => fileName;
            set => fileName = value;
        }
        /// <summary>
        /// the main accessor for the documentFont Property
        /// </summary>
        public PDFGenerator.DocumentFont DocumentFont
        {
            get => documentFont;
            set => documentFont = value;
        }
        /// <summary>
        /// the main accessor for the fontSize Property
        /// </summary>
        public Double FontSize
        {
            get => fontSize;
            set => fontSize = value;
        }
        /// <summary>
        /// the main accessor for the documentType Property
        /// For More info check the property
        /// </summary>
        public PDFGenerator.DocumentType DocumentType
        {
            get => documentType;
            set => documentType = value;
        }
        #endregion
    }
}
