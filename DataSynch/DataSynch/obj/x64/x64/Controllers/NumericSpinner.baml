   M S B A M L   `   `   `  ���� D  @DataSynch, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null=#clr-namespace:DataSynch.ControllersDataSynch.Controllers  *    $DataSynch.Controllers.NumericSpinner   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     5local#clr-namespace:DataSynch.Controllers     -     >�Name$  root_numeric_spinner�� � 	Resources    updown_button_style �� 
TargetType ��Name  ThemeBrushDark  
ThemeBrush �� CornerRadius ��Name �� Triggers P� Property P� Value	 �� 
TargetName  ThemeBrushActive
 �� Value �� Property �� 
TargetType��%  &	        '�� �     7   �� " ��.R��� $ br��$��0q�#���  #���  $ 0��._��� $ cp��#��z��#��z��#��z�� P�  �@J�IsMouseOver . . ��� $	 br��#
 �   � ��P�  � {� .  . ��� $	 br�� � ��

 ������� " ��.���  � ��8
   .����  J� OpacityMask !�  !� Visual �� 
���   � J� SnapsToDevicePixels . $ 4������ *ActualWidth+ �� RelativeSource �� *FindAncestor+ �� AncestorType$ BorderL����� *ActualHeight+ �� *FindAncestor+$ BorderL�._��  � RowDefinitions �� ��  � ColumnDefinitions �� �� $��22 �.+��� -    ��Name$ tb_main��$��Medium"�$��14(�$��Center=�$	��5,1q�$��0��$��2��$��0�� �� 
IsReadOnly . �� -    ��Name$ cmdUp��$��1��$��0��$
��auto��$
��auto����F� $��Center=�$��Center=�
���   �"���� 	=     �     	= � z  	= 	�� -   $ cmdDown��$��1��$��1��$
��auto��$
��auto����F� $��Center=�$��Center=�
���   �"����        � z     � 	=  	= 	�� 
������� J� IsHitTestVisible .  $��1q�$ 4��$��2��$��2�� �� Padding$ 0q�$��0q�