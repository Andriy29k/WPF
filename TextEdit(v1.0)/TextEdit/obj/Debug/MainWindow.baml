   M S B A M L   `   `   `  ����C  ?TextEdit, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null#clr-namespace:TextEditTextEdit  5   	       TextEdit.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   (localclr-namespace:TextEdit     5   	   -   5          � Title$  Text Editor (v 1.0)��5   	   $	��350��6%   $	��520��62    � WindowStartupLocation$ CenterScreen=�6>    J� CommandBindings 5
      )  #System.Windows.Input.CommandBinding  5   
   -   5        	exit0��   Command$ ApplicationCommands.Close��5   (   5        5   
   -   5        new0��$ ApplicationCommands.New��5   '   5        5   
   -   5        	open0��$ ApplicationCommands.Open��5   (   5        5   
   -   5        	save0��$ ApplicationCommands.Save��5   (   5   #     5   
   -   5        saveAs0��$  ApplicationCommands.SaveAs��5   *   5      .��5      ]� .+�5   
   o� $	��Top=�6   .@�5      m� -   5         m�Name$ file1��5      $��_File��6&   .@�5      m� -   5        $
 new1��5      $
��_New��6)    m� Command$ ApplicationCommands.New��68   m� 5      -	   5        $ open1��5      $��_Open��6*   $ ApplicationCommands.Open��6:   m� 5      -
   5        $ save1��5      $��_Save��6*   $ ApplicationCommands.Save��6:   m� 5      -   5        $ saveAs1��5      $��
Save as...��6,   $  ApplicationCommands.SaveAs��6A   �� 5      m� 5      -   5        $ exit1��5      $��E_xit��6*    m� InputGestureText$ Alt+F4��69   $ ApplicationCommands.Close��5      5       5!      �� 5"   
   -   5         ��Name$ textBox1��5"      $�� ��6$    �� AcceptsReturn . "	 �� VerticalScrollBarVisibility$
	 Auto=�6A   $
 �� HorizontalScrollBarVisibility$

 Auto=�5#       �� 
AcceptsTab . 5$      5%      5&      