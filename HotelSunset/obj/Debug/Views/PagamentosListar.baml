   M S B A M L   `   `   `  ����F  BHotelSunset, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:HotelSunset.ViewsHotelSunset.Views  5   	   (    "HotelSunset.Views.PagamentosListar   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   1localclr-namespace:HotelSunset.Views     5   	     � Title$  Listar Pagamentos��5   	   $	��480��6#   $	��750��60    � 
ResizeMode$ NoResize=�5	   	    � WindowStartupLocation$ CenterScreen=�5
   	    � 	Resources 5         
LabelStyle �� 
TargetType �� Property �� Value  InputBaseStyle �� BasedOn(  "System.Windows.Controls.DatePicker  ActionButtonStyle �� 
TargetType	 �� CornerRadius
 �� Triggers  
BackButton �� Triggers P� Property P� Value  	NewButton  
EditButton  DeleteButton��%S  &	        &	 �     '�� ,    7  '�� N    7  '  p    7  &	 �    &	 [    7  &	     7  &	     7  &	 �    7  �� 5   
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
   " ��6"   8   .�5D      ��  � �� �#28A7456+   �� 5E       � ��$ 0,0,10,0q�6'    5F      P� 5G       �@J�IsMouseOver . . �5H      ��  � �� �#21883863   5I      5K      �� 5M   
   " ��6#   8   .�5N      ��  � �� �#007BFF6+   �� 5O       � ��$ 0,0,5,0q�6'    5P      P� 5Q       �@J�IsMouseOver . . �5R      ��  � �� �#0056B363   5S      5U      �� 5W   
   " ��6%   8   .�5X      ��  � �� �#DC35456+   �� 5Y       � ��$ 5,0,0,0q�6'    5Z      P� 5[       �@J�IsMouseOver . . �5\      ��  � �� �#C8233363   5]      5_      5a      .��5c      � ��5d   
   �� 5e       �� 
StartPoint$	 0,0&�6"    �� EndPoint$	 1,1&�63   .��5f      �  � Color$ #FDE68A��6    � Offset$ 0Q�6/   � 5g      $ #FCA5A5��6   $ 1Q�6/   5h       � RowDefinitions 5k   
   �� 5l      $
��Auto �6   �� 5m      $��* �6   �� 5n      $
��Auto �6   .+�5q   
   � $��0��6   $��50��6   5r      �� 5t   
   $��0��6   $��Center=�6!   $��Center=�6>   $��20(�5u      $
��Bold"�6"   ���#8B4513$��0,0,0,15q�6I   �� 5v       � Language$ pt-br�6   $�Lista de Pagamentos��6#   5w      �� 5y   
   $��1��6   
�������$	 10��61    �� Padding$ 10q�6C   $��Center=�5z      $��Stretch=�6.   $��	0,10,0,10q�6J   $	��700��6]    J� Effect 5{      3  -System.Windows.Media.Effects.DropShadowEffect  5|         ShadowDepth$ 4Q�6#      	Direction$	 270Q�63      Color$ #000000��6C      Opacity$	 0.2Q�6S      
BlurRadius$ 8Q�6a   ._�5      &   System.Windows.Controls.DataGrid  -   5          Name$ PagamentosDataGrid��5         AutoGenerateColumns .     
IsReadOnly .    HeadersVisibility$ Column=�5�         SelectionMode$ Single=�62   
����������#D1D5DB$��1q�5�   @       RowBackground
  �����!   AlternatingRowBackground! �#F9F9F9"   GridLinesVisibility$" 
Horizontal=�5�      #   HorizontalGridLinesBrush# �#EEEEEE$��12(�5�      $	��300��5�      $��Stretch=�5�      $��Stretch=�63   $   CanUserAddRows$ .  %   Columns% 5�      0  *System.Windows.Controls.DataGridTextColumn  5�      ,  &System.Windows.Controls.DataGridColumn&   Header$& ID��6)   '   Width5  /System.Windows.Controls.DataGridLengthConverter$
' Auto 6U   1  +System.Windows.Controls.DataGridBoundColumn(   Binding( 65   �� *IdPagamento+6U     5�      $& Despesa��6)   $' * 6Z   ( 6:   �� *NomeDespesa+6Z     5�      $
& Data��6)   $' * 6t   ( 67   �� *DataPagamento+) �� StringFormat$) 
dd/MM/yyyy��6t     5�      $& Parcela��6)   $
' Auto 6\   ( 6:   �� *NumeroParcela+6\     5�      $& Valor��6)   $
' Auto 6l   ( 68   �� *ValorPagamento+$) C2��6l     5�      $& Tipo Pagamento��6)   $' * 6c   ( 6A   �� *TipoPagamento+6c   4  .System.Windows.Controls.DataGridTemplateColumn  5�      $
' Auto 6-   $& Ações��6:   *   CellTemplate* 5�      �� 5�      .R�5�   "   �� + �� Orientation$+ 
Horizontal=�6-   $��Center=�6F   $��Center=�6c   .+�5�   &   �� $��Editar��6-   #��[ 6>   $��70��6b   $��25��6m   �� 5�   &   $��Excluir��6-   #��[ 6?   $��70��6e   $��25��6p   5�   #   5�      5�      5�      5�      �� 5�   
   $��2��6   $+ 
Horizontal=�6"   $��Right=�6;   $��
0,10,20,10q�6W   .+�5�      �� $
��Novo��6   #��[ 6$   $��80��6G   $��30��6R   �� 5�      $��Voltar��6   #��[ 6&   $��80��6J   $��30��6U   5�      5�      5�      