   M S B A M L   `   `   `  ���� D  @DataSynch, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:DataSynch.SettingsDataSynch.Settings  =#clr-namespace:DataSynch.ControllersDataSynch.Controllers  %    DataSynch.Settings.SettingsForm   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     2local clr-namespace:DataSynch.Settings     3app#clr-namespace:DataSynch.Controllers     .
extensionsusing:Microsoft.Toolkit       � Title$  Settings��$	��350��$	��500�� � Cursor$ Arrow�� � WindowStyle$
 None=� � ShowInTaskbar .   � WindowStartupLocation$ CenterScreen=� � 
ResizeMode$ NoResize=�.���    MainGradientStyle#���   .+�� -    �Name$ 
grTitleBar��$��60��$	��Top=�$��Stretch=����#2d2d2d.+��� 
�������$��Setari Program��$��25(�  DefaultFont#��[ $��Center=�$
��Left=�$��10,0,0,0q�A� -   $��35��$��35��$	��Top=�$��Right=�$��	0,10,10,0q�����  �� ImageSource$ ..\images\close.jpg��� $��0,60,0,0q�.+��� -    ��Name$ lblClientGuid��$��GUID Client��$��13(�#��[ $��Center=�$��5,10,350,245q��� -   $ lblWorkPointGuid��$��GUID Punct Lucru��$��13(�#��[ $��Center=�$��5,50,350,205q��� $��Interval de Upload��$��13(�#��[ $��Center=�$��5,90,350,165q��� $��Interval de Download��$��13(�#��[ $��Center=�$��5,130,350,125q��� -   	 ��Name$	 tbClientGUID��$��13(�#��[ $��Center=�$��155,10,50,245q�
 �� 	MaxLength$
 36��$��Center=��� -   $	 tbWorkStationGUID��$��13(�#��[ $��Center=�$��155,50,50,205q�$
 36��$��Center=�*   $DataSynch.Controllers.NumericSpinner  -     Name$ spUpload��$	��100��$��35��$��155,90,245,165q�   Value$
 1000~�   Step$
 1000~�   MinValue$
 1000~�   MaxValue$ 60000~�$��13(�#��[   -   $ 
spDownload��$	��100��$��35��$��155,130,245,125q�$
 1000~�$
 1000~�$
 1000~�$ 60000~�$��13(�#��[ �� $��ms��$��35��$��35��$��Center=�$��195,90,245,165q�#��[ $��13(��� $��ms��$��35��$��35��$��Center=�$��195,130,245,125q�#��[ $��13(��� -	    ��Name$ 	btnAccept��$��13(�#��[   ButtonStyle#��[ $��125,235,260,10q�$��Salveaza���� -
   $ 	btnCancel��$��13(�#��[ #��[ $��260,235,125,10q�$��Renunta��