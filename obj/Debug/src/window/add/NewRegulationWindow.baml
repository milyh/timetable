   M S B A M L   `   `   `  ����  	timetable2    ,timetable.src.window.add.NewRegulationWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   X TXceed.Wpf.Toolkit, Version=1.9.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit  5   	   -   5          � Title$%  Добавить правила��5   	    � 
ResizeMode$ NoResize=�6"    � SizeToContent$ WidthAndHeight=�68    � WindowStartupLocation$ CenterOwner=�6W   .��5      � $��10q�6    � RowDefinitions 5	   
   �� 5
      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6    � ColumnDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   .+�5   
   �� $��0��6   $��0��6    $ ��Преподователь��6-   $��0,0,10,0q�6E   $��Right=�6W   $��Center=�6s   $��14(�6�   �� 5   
   -   5        $��teacherNameComboBox��5      $��1��6.   $��0��6>   $	��150��6K    �� 
IsEditable . $��Center=�6i   6�   �� 5   
   $��0��6   $��1��6    $��День недели��6-   $��0,0,10,0q�6C   $��Right=�6U   $��Center=�6q   $��14(�6�   �� 5   
   -   5        $��dayOfWeekComboBox��5      $��1��6,   $��1��6<   $	��150��6I    . $��Center=�6g   �� 5   
   $��0��6   $��2��6    $��Занятия��6-   $��0,0,10,0q�6?   $��Right=�6Q   $	��Top=�6m   $��14(�6�   $  Xceed.Wpf.Toolkit.CheckListBox  5   
   -   5        $��lessonsLayout��5   9   $��1��6   $��2��6,   ��6N   Z� 6e   �� 5!   
   $��0��6   $��3��6$   $B��<Максимальное количество занятий��61   $��0,0,10,0q�6X   $��Right=�6j   $��Center=�6�   $��14(�6�   $	��130��6�   $
��Wrap=�6�   �� 5"   
   -   5        $��maxLessonsComboBox��5"      $��1��6-   $��3��6=   $	��150��6J    . $��Center=�6h   {�.  6�   �� 5$   
   $��0��6   $��4��6%    �� Orientation$ Vertical=�62   $��0,0,10,0q�6I   $��Right=�6[   $	��Top=�6w   .+�5%      �� $��Комментарий��6   $��Right=�6*   $��Center=�6F   $��5,5,5,2q�6a   $��14(�6s   �� 5&      $#��(не обязательно)��6   $��Center=�6/   $��Center=�6L   $��10(�6g   $��Center=�6u   $��Center=�6�   $��0q�6�   5'      �� 5(   
   -   5        $��descriptionTextBox��5(      $��1��6-   $��4��6=   $	��150��6J    �� TextWrapping$
 Wrap=�6V   $��60��6j   $��Center=�6y   $��14(�6�   �� 5*   
   -   5        $��	addButton��5*      $��2��68   $��5��6L   $��Добавить��6Y   $��Right=�6l   $��Center=�6�   $��Center=�6�   $��0,10,0,0q�6�   $��3q�6�   $y�	Collapsed=�6�   �� 5,   
   -   5        $��errorTextBox��5,      $��3��6&   $��6��66   $��14(�6G   $
 Wrap=�6U   $	��250��6i   $��10,0,0,0q�6x   $��Stretch=�6�   $��Center=�6�   $��Center=�6�   $��2q�6�   ���	#FFFF002E	 �� SelectionBrush	 �	#FFFF002E
 �� 
IsReadOnly
 . $y�	Collapsed=�6?  5.      5/      