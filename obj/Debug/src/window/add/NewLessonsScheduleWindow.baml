   M S B A M L   `   `   `  ����  	timetable7    1timetable.src.window.add.NewLessonsScheduleWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   X TXceed.Wpf.Toolkit, Version=1.9.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit  5   	   -   5          � Title$4  .Новое расписание звонков��5   	    � 
ResizeMode$ NoResize=�6*    � SizeToContent$ WidthAndHeight=�6@    � WindowStartupLocation$ CenterOwner=�6�    +� FocusedElement 6_   ��  �� ElementName$ beginTimePicker��.��5      � $��10q�6    � RowDefinitions 5	   
   �� 5
      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6    � ColumnDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   .+�5   
   �� $��0��6   $��0��6    $!��Начало занятия��6-   $��0,0,10,0q�6F   $��Right=�6X   $��Center=�6t   $��14(�6�   "  Xceed.Wpf.Toolkit.TimePicker  5   
   -   5        $��beginTimePicker��5      $��1��61   $��0��6A   $��80��6N      Format$ 	ShortTime=�5      $��Center=�5      $��Center=�65   $��Center=�6Y   �� 5   
   $��0��6   $��1��6    $'��!Окончание занятия��6-   $��0,0,10,0q�6I   $��Right=�6[   $��Center=�6w   $��14(�6�     5   
   -   5        $��endTimePicker��5      $��1��6/   $��1��6?   $��80��6L   $ 	ShortTime=�5      $��Center=�5      $��Center=�65   $��Center=�6Y   �� 5    
   $��0��6   $��2��6    $��Номер занятия��6-   $��0,0,10,0q�6E   $��Right=�6W   $��Center=�6s   $��14(�6�   �� 5!   
   -   5        $��numberLesson��5!   ;   $��1��6   $��2��6#   $��80��60   $��Center=�6O   �� 5#   
   $��0��6   $��3��6%   	 �� Orientation$	 Vertical=�62   $��0,0,10,0q�6I   $��Right=�6[   $	��Top=�6w   .+�5$      �� $��Комментарий��6   $��Right=�6*   $��Center=�6F   $��5,5,5,2q�6a   $��14(�6s   �� 5%      $#��(не обязательно)��6   $��Center=�6/   $��Center=�6L   $��10(�6g   $��Center=�6u   $��Center=�6�   $��0q�6�   5&      �� 5'   
   -   5        $��descriptionTextBox��5'      $��1��6-   $��3��6=   $	��150��6J   
 �� TextWrapping$

 Wrap=�6V   $��60��6j   $��Center=�6y   $��14(�6�   �� 5)   
   -   5        $��	addButton��5)      $��2��6=   $��4��6Q   $��Добавить��6^   $��Right=�6q   $��Center=�6�   $��Center=�6�   $��0,10,0,0q�6�   $��3q�6�   �� 5+   
   -   5        $��errorTextBox��5+      $��3��6&   $��5��66   $��14(�6G   $

 Wrap=�6U   $	��250��6i   $��10,0,0,0q�6x   $��Stretch=�6�   $��Center=�6�   $��Center=�6�   $��2q�6�   $y�	Collapsed=�6�   ���	#FFFF002E �� SelectionBrush �	#FFFF002E �� 
IsReadOnly . 6D  5-      5/      