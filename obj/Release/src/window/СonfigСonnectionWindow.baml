   M S B A M L   `   `   `  ����   	timetable3    -timetable.src.window.СonfigСonnectionWindow   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     -     � Title$  Настройки�� � SizeToContent$ WidthAndHeight=� � 
ResizeMode$ CanMinimize=� � WindowStartupLocation$ CenterScreen=� � Icon$, &/timetable;component/image/setting.png��.��� $��5q�$
��Left=� � RowDefinitions �� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto � � ColumnDefinitions �� $
��Auto ��� $
��Auto �.+��� $��Server / Host:��$��0��$��0��$��Right=�$	��Top=�$��0,0,5,5q�$��0,5,5,5q��� -   $��serverTextBox��$�� ��$��0��$��1��$
��Left=� �� TextWrapping$ NoWrap=�$	��120��$��Center=�$��Bottom=�$	��0,2q�$��0,0,0,5q��� $��Port:��$��1��$��0��$��Right=�$	��Top=�$��0,0,5,5q�$��0,5,5,5q��� -   $��portTextBox��$�� ��$��1��$��1��$
��Left=�$ NoWrap=�$	��120��$��Center=�$��Center=�$	��0,2q��� $��	Database:��$��2��$��0��$��Right=�$	��Top=�$��0,0,5,5q�$��0,5,5,5q��� -   $��dataBaseTextBox��$�� ��$��2��$��1��$
��Left=�$ NoWrap=�$	��120��$��Center=�$��Center=�$	��0,2q��� $��User:��$��3��$��0��$��Right=�$	��Top=�$��0,0,5,5q�$��0,5,5,5q��� -   $��userTextBox��$�� ��$��3��$��1��$
��Left=�$ NoWrap=�$	��120��$��Center=�$��Center=�$	��0,2q��� $��	Password:��$��4��$��0��$��Right=�$	��Top=�$��0,0,5,5q�$��0,5,5,5q�G� -   $��passwordTextBox��$��4��$��1��$
��Left=�$	��120��$��Center=�$��Center=�$	��0,2q��� -   $��errorTextBox��$��5��$��0��$��0��$!��нет соединения��$
 Wrap=�$��Right=�$��Center=����	#FFB04242$��Center=�$��Center=���Z� ��Z� �� -   $��
saveButton��$�� Сохранить ��$��5��$��1��$��Right=�$��Center=�$��0,10,5,5q�$��13(�$��Stretch=�$��Stretch=�9� $5��/Дополнительные настройки��$��6��$��2��$��Stretch=�$��0,15,0,0q�.���  �� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto � �� $
��Auto ��� $
��Auto �.+��� $��	Protocol:��$��0��$��0��$��Right=�$	��Top=�$��0,0,5,5q�$��0,5,5,5q��� -	   $��protocolTextBox��$�� ��$��0��$��1��$
��Left=�$ NoWrap=�$	��120��$��Center=�$��Bottom=�$	��0,2q�$��0,0,0,5q��� $��Integrated security:��$��1��$��0��$��Right=�$	��Top=�$��0,0,5,5q�$��0,5,5,5q��� -
   $��securityCheckBox��$��1��$��1��$��Center=�$��Center=�$��Center=��� $
��SSL:��$��2��$��0��$��Right=�$	��Top=�$��0,0,5,5q�$��0,5,5,5q��� -   $��sslCheckBox��$��2��$��1��$��Center=�$��Center=�$��Center=��� $��Pooling:��$��3��$��0��$��Right=�$	��Top=�$��0,0,5,5q�$��0,5,5,5q��� -   $��poolingCheckBox��$��3��$��1��$��Center=�$��Center=�$��Center=��� $��Min pool size:��$��4��$��0��$��Right=�$	��Top=�$��0,0,5,5q�$��0,5,5,5q��� -   $��minPoolSizeTextBox��$�� ��$��4��$��1��$
��Left=�$ NoWrap=�$	��120��$��Center=�$��Bottom=�$	��0,2q�$��0,0,0,5q��� $��Max pool size:��$��5��$��0��$��Right=�$	��Top=�$��0,0,5,5q�$��0,5,5,5q��� -   $��maxPoolSizeTextBox��$�� ��$��5��$��1��$
��Left=�$ NoWrap=�$	��120��$��Center=�$��Bottom=�$	��0,2q�$��0,0,0,5q��� $��Timeout:��$��6��$��0��$��Right=�$	��Top=�$��0,0,5,5q�$��0,5,5,5q��� -   $��timeoutTextBox��$�� ��$��6��$��1��$
��Left=�$ NoWrap=�$	��120��$��Center=�$��Bottom=�$	��0,2q�$��0,0,0,5q��� $��Command timeout:��$��7��$��0��$��Right=�$	��Top=�$��0,0,5,5q�$��0,5,5,5q��� -   $��commandTimeoutTextBox��$�� ��$��7��$��1��$
��Left=�$ NoWrap=�$	��120��$��Center=�$��Bottom=�$	��0,2q�$��0,0,0,5q��� $��	SSL mode:��$��8��$��0��$��Right=�$	��Top=�$��0,0,5,5q�$��0,5,5,5q��� -   $��sslModeComboBox��$��8��$��1��$��Center=� �� SelectedIndex$ 0��.@��� -   $��Disable��$��Disable���� -   $��Prefer��$��Prefer���� -   $��Require��$��Require���� $��Connection life time:��$��9��$��0��$��Right=�$	��Top=�$��0,0,5,5q�$��0,5,5,5q��� -   $��connectionLifeTimeTextBox��$�� ��$��9��$��1��$
��Left=�$ NoWrap=�$	��120��$��Center=�$��Bottom=�$	��0,2q�$��0,0,0,5q��� $��Sync notification:��$��10��$��0��$��Right=�$	��Top=�$��0,0,5,5q�$��0,5,5,5q��� -   $��syncNotificationCheckBox��$��10��$��1��$��Center=�$��Center=�$��Center=��� $��Enlist:��$��11��$��0��$��Right=�$	��Top=�$��0,0,5,5q�$��0,5,5,5q��� -   $��enlistCheckBox��$��11��$��1��$��Center=�$��Center=�$��Center=�