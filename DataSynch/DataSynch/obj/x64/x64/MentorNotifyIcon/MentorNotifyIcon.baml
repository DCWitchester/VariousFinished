   M S B A M L   `   `   `  ���� D  @DataSynch, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullG(clr-namespace:DataSynch.MentorNotifyIconDataSynch.MentorNotifyIcon  1    +DataSynch.MentorNotifyIcon.MentorNotifyIcon   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     :local(clr-namespace:DataSynch.MentorNotifyIcon       � Title$  MentorNotifyIcon��$	��300�� � SizeToContent$ Height=� � WindowStyle$
 None=� � AllowsTransparency . 
������ .���  J� RenderTransformOrigin$	 0,1&�.+��� $��1q�
�������
���   � �� CornerRadius$ 10��._��� $��20q�.+��� $
��Wrap=�$��5q��� .�Notification data�� �� ;9Something just happened and you are being notified of it. � Triggers :�  :� RoutedEvent$ FrameworkElement.Loaded��.[��� .���� .��U�  �� TargetProperty$9 3(UIElement.RenderTransform).(ScaleTransform.ScaleY)�.\��� 	 O� KeyTime$	 0:0:0��
 O� Value$
 0Q��� $	 0:0:0.5��$
 1Q�U� $ (UIElement.Opacity)�.\��� $	 0:0:2��$
 1Q��� $	 0:0:4��$
 0Q�z���  �� ScaleY$ 1Q�