   M S B A M L   `   `   `  ����F  BHotelSunset, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:HotelSunset.ViewsHotelSunset.Views  5   	   ,    &HotelSunset.Views.FuncionarioConsultar   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   1localclr-namespace:HotelSunset.Views     5   	     � Title$  Editar Funcionário��5   	   $	��480��6$   $	��750��61    � 
ResizeMode$ NoResize=�5	   	    � WindowStartupLocation$ CenterScreen=�5
   	    � 	Resources 5         
LabelStyle �� 
TargetType �� Property �� Value  InputBaseStyle �� BasedOn(  "System.Windows.Controls.DatePicker  ActionButtonStyle �� 
TargetType	 �� CornerRadius
 �� Triggers  
BackButton �� Triggers P� Property P� Value  ClearButton  
SaveButton  
EditButton��%   &	        &	 �     '�� ,    7  '�� N    7  '  p    7  &	 �    &	 [    7  &	     7  &	 �    7  &	 �    7  �� 5   
   " ��6#   .�5      ��  � �� �#3333336+   �� 5       � ��$
 Bold"�6+   �� 5       � ��$ 0,0,0,4q�6'   5      �� 5   
   " ��6'   .�5      ��  � ��$ 5q�6(   �� 5       � �� �#D1D5DB6,   �� 5       � ��$ 1q�60   �� 5       � ��
 �����6+   �� 5       � ��$ Center=�69   �� 5       � ��$ 30��6'   �� 5       � ��$ 0,0,0,8q�6'   �� 5       �@J�SnapsToDevicePixels . 64   �� 5       � �� �#3333336+   5      �� 5   
   " ��6   8   6%   �� 5    
   " ��6   8   6&   �� 5"   
   "  6   8   6(   �� 5$   
   " ��6*   .�5%      ��  � ��
 �����6+   �� 5&       � ��$
 Bold"�6+   �� 5'       � ��$
 10,5q�6(   �� 5(       � ��$ 0,0,10,0q�6'   �� 5)       � ��$ 0q�60   �� 5*       � �� 5+      �� 5,      " ��6&   .R�5-      �� #��z��6!   $	 5��5.   !   #��z��5/   !   #��z��50   !   ._�51      �� $��Center=�6/   $��Center=�6L   52      
 53      55      57      58      �� 5:   
   " ��6#   8   .�5;      ��  � �� �#6B72806+    5<      P� 5=       �@J�IsMouseOver . . �5>      ��  � �� �#4A556863   5?      5A      �� 5C   
   " ��6$   8   .�5D      ��  � �� �#9CA3AF6+    5E      P� 5F       �@J�IsMouseOver . . �5G      ��  � �� �#6B728063   5H      5J      �� 5L   
   " ��6#   8   .�5M      ��  � �� �#F973166+    5N      P� 5O       �@J�IsMouseOver . . �5P      ��  � �� �#C2410C63   5Q      5S      �� 5U   
   " ��6#   8   .�5V      ��  � �� �#007BFF6+   �� 5W       � ��$ 0,0,5,0q�6'    5X      P� 5Y       �@J�IsMouseOver . . �5Z      ��  � �� �#0056B363   5[      5]      5_      .��5a      � ��5b   
   �� 5c       �� 
StartPoint$	 0,0&�6"    �� EndPoint$	 1,1&�63   .��5d      �  � Color$ #FDE68A��6    � Offset$ 0Q�6/   � 5e      $ #FCA5A5��6   $ 1Q�6/   5f       � RowDefinitions 5i   
   �� 5j      $
��Auto �6   �� 5k      $��* �6   .+�5o   
   �� $��1��6   
�������$	 10��61    �� Padding$ 10q�6C   $��Center=�5p      $��Center=�6.   $	��370��6I    J� Effect 5q      3  -System.Windows.Media.Effects.DropShadowEffect  5r         ShadowDepth$ 4Q�6#      	Direction$	 270Q�63      Color$ #000000��6C      Opacity$	 0.2Q�6S      
BlurRadius$ 8Q�6a   ._�5u      �� $	��650��6   .+�5v      �� $��20(�6   $
��Bold"�6*   ���#8B4513$��0,0,0,15q�6Q   �� 5w       � Language$ pt-br�6   $�Detalhes do Funcionário��6+   5x      � 5z      $��0,0,0,10q�6    � ColumnDefinitions 5{      �� 5|      $��* �6+   �� 5}      $��* �6+    5      �� 5�      $
��Auto �6(   �� 5�      $
��Auto �6(   �� 5�      $
��Auto �6(   �� 5�      $
��Auto �6(   .+�5�      �� $��0��6!   $��0��6.   $��0,0,10,0q�6>   .+�5�      �� $��Nome:��6$   #��[  61   �� 5�      -   5         ��Name$ txtNome��5�   "   {�.  ��63   �� *NomeFuncionario+6T   5�      �� 5�      $��0��6!   $��1��6.   .+�5�      �� $
��CPF:��6$   #��[  60   �� 5�      -   5        $ txtCPF��5�   "   {�.  ��62   �� *CpfFuncionario+6R   5�      �� 5�      $��1��6!   $��0��6.   $��0,0,10,0q�6>   .+�5�      �� $��Data de Nascimento:��6$   #��[  6?     5�      -   5          Name$ dtpNascimento��5�   %      SelectedDateFormat$ Short=�6=   {�.     SelectedDate 6X   �� *DataNascimentoFuncionario+6�   5�      �� 5�      $��1��6!   $��1��6.   .+�5�      �� $	��RG:��6$   #��[  6/   �� 5�      -   5        $ txtRG��5�   "   {�.  ��61   �� *RgFuncionario+6P   5�      �� 5�      $��2��6!   $��0��6.   $��0,0,10,0q�6>   .+�5�      �� $��	Salário:��6$   #��[  64   �� 5�      -   5        $ 
txtSalario��5�   "   {�.  ��66   �� *SalarioFuncionario+6Z   5�      �� 5�      $��2��6!   $��1��6.   .+�5�      �� $��Email:��6$   #��[  62   �� 5�      -   5        $ txtEmail��5�   "   {�.  ��64   �� *EmailFuncionario+6V   5�      �� 5�      $��3��6!   $��0��6.   $��0,0,10,0q�6>   .+�5�      �� $��	Telefone:��6$   #��[  65   �� 5�      -   5        $ txtTelefone��5�   "   {�.  ��67   �� *TelefoneFuncionario+6\   5�      �� 5�      $��3��6!   $��1��6.   5�      5�      �� 5�        �� Orientation$  
Horizontal=�6   $��Right=�66   $��0,20,0,0q�6R   .+�5�      �� -   5        ! ��Name$! btVoltar��5�      $��Voltar��6/   #��[ 6@   $��80��6d   $��30��6o   6{   �� 5�      -	   5        $! btLimpar��5�      $��Limpar��6/   #��[ 6@   $��30��6e   $��80��6q   6|   �� 5�      -
   5        $! btEditar��5�      $��Editar��6/   #��[ 6@   $��30��6d   $��80��6p   6{   5�      5�      5�      5�      5�      