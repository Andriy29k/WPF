   M S B A M L   `   `   `  ����D  @Listboxes, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:Listboxes	Listboxes  5   	       Listboxes.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   )localclr-namespace:Listboxes     5   	     � Title$  	Listboxes��5   	    � SizeToContent$ WidthAndHeight=�6    � WindowStartupLocation$ CenterScreen=�6:    � 
ResizeMode$ CanMinimize=�6_   .��5	      �� $��5q�6    �� Orientation$ 
Horizontal=�6   .+�5
   
   �� .+�5      �� -   5         ��Name$ 	comboBox1��5      $��5q�6*   $	��150��65   5      �� 5      -   5         ��Name$ rect1��5      $��5q�6'   $	��100��62   
���   ���5      ��  �� ElementName$ 	comboBox1�� �� Path$ SelectedValue��� 5      -   5        $ rect2��5      $��5q�6'   $	��100��62   
���   �6?   5      �� 5   
   	 � 	Resources	 5       	  label
 �� Property �� Value��%�   &	        �� 5      .�5      �� 
 � ��$ 5q�68   �� 5      
 � ��$ 5q�67   �� 5      
 � ��
 �   �6<   �� 5      
 � ��$ 1q�6@   �� 5      
 � ��$ Center=�6K   5      5      .+�5      �� -   5         ��Name$ label1��5      $	��Add��6$   #��[  5   2   �� 5      -   5        $ label2��5      $��Delete��6$   #��[  5      �� 5       -   5        $ label3��5       $��Insert��6$   #��[  5!   1   �� 5"      -   5        $ label4��5"      $��Move Up��6$   #��[  5#      �� 5$      -   5        $ label5��5$      $��	Move Down��6$   #��[  5%   1   �� 5&      -	   5        $ label6��5&      $��Save To File��6$   #��[  5'   1   �� 5(      -
   5        $ label7��5(      $��Load From File��6$   #��[  5)      5*      �� 5+   
   -   5         ��Name$ listBox1��5+      $��5q�6$   $	��150��6/    J� 	AllowDrop . 5-      5.      5/      