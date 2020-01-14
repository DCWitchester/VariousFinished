using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WorkOrder.Messages;

namespace WorkOrder
{
    /// <summary>
    /// Interaction logic for MessageForm.xaml
    /// </summary>
    public partial class MessageForm : Window
    {
        public MessageForm()
        {
            InitializeComponent();
        }
        //the button press loads the messageFormReturn variable based on the sender ( true for accept; false for cancel )
        public void ButtonPress(object sender, RoutedEventArgs e)
        {
            MessageSettings.messageFormReturn = sender.Equals(this.btnAccept);
            this.Close();
        } 
    }
}
