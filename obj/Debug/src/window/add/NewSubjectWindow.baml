   M S B A M L   `   `   `  ����  	timetable/    )timetable.src.window.add.NewSubjectWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   X TXceed.Wpf.Toolkit, Version=1.9.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit  5   	   -   5          � Title$  Новый предмет��5   	    � 
ResizeMode$ NoResize=�6    � SizeToContent$ WidthAndHeight=�65    � WindowStartupLocation$ CenterOwner=�6�    +� FocusedElement 6T   ��  �� ElementName$ subjectNameTextBox��.��5	      � $��10q�6    � RowDefinitions 5
   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6    � ColumnDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   .+�5   
   �� $��0��6   $��0��6    $'��!Название предмета��6-   $��0,0,10,0q�6I   $��Right=�6[   $��Center=�6w   $��14(�6�   �� 5   
   -   5        $��subjectNameTextBox��5      $��1��6,   $��0��6<   $	��150��6I   $��Center=�6U   $��14(�6p   �� 5   
   $��0��6   $��1��6%    �� Orientation$ Vertical=�62   $��0,0,10,0q�6I   $��Right=�6[   $	��Top=�6w   .+�5      �� $��Аудитория��6   $��Right=�6(   $��Center=�6D   $��5,5,5,2q�6_   $��14(�6q   �� 5      $<��6(возможно несколько значений)��6   $��Center=�6<   $��Center=�6Y   $��10(�6t   $��Center=�6�   $��Center=�6�   $��0q�6�   5      %  Xceed.Wpf.Toolkit.CheckComboBox  5   
   -   5        $��classesComboBox��5      $��1��64   $��1��6D   $��Center=�6Q   +  %Xceed.Wpf.Toolkit.Primitives.Selector	   	Delimiter$	 , ��6l   �� 5   
   $��0��6   $��2��6%   $ Vertical=�62   $��0,0,10,0q�6I   $��Right=�6[   $	��Top=�6w   .+�5       �� $��Комментарий��6   $��Right=�6*   $��Center=�6F   $��5,5,5,2q�6a   $��14(�6s   �� 5!      $#��(не обязательно)��6   $��Center=�6/   $��Center=�6L   $��10(�6g   $��Center=�6u   $��Center=�6�   $��0q�6�   5"      �� 5#   
   -   5        $��descriptionTextBox��5#      $��1��6-   $��2��6=   $	��150��6J   
 �� TextWrapping$

 Wrap=�6V   $��60��6j   $��Center=�6y   $��14(�6�   �� 5%   
   -   5        $��	addButton��5%      $��2��65   $��3��6I   $��Добавить��6V   $��Right=�6i   $��Center=�6�   $��Center=�6�   $��0,10,0,0q�6�   $��3q�6�   $y�	Collapsed=�6�   �� 5'   
   -   5        $��errorTextBox��5'      $��3��6&   $��4��66   $��14(�6G   $

 Wrap=�6U   $	��250��6i   $��10,0,0,0q�6x   $��Stretch=�6�   $��Center=�6�   $��Center=�6�   $��2q�6�   $y�	Collapsed=�6�   ���	#FFFF002E �� SelectionBrush �	#FFFF002E �� 
IsReadOnly . 6D  5)      5+      