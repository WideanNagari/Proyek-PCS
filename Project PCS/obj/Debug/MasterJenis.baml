   M S B A M L   `   `   `  ����F  BProject PCS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:Project_PCSProject_PCS  5   	       Project_PCS.MasterJenis   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35n jSAPBusinessObjects.WPF.ViewerShared, Version=13.0.3500.0, Culture=neutral, PublicKeyToken=692fbea5521e1304R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   +localclr-namespace:Project_PCS     5   	   -   5          � Title$  MasterJenis��5   	   $	��600��6   $
��1000��6*    � WindowStyle$
 None=�6N    � 
ResizeMode$ NoResize=�5	   	    � WindowStartupLocation$ CenterScreen=�6    � 	Resources 5
         TextBoxBaseControlTemplate �� 
TargetType ��Name �� CornerRadius �� Padding	 ��Name
 �� Triggers P� Property P� Value �� Value �� 
TargetName �� Property  RowStyleWithAlternation �� 
TargetType)  #System.Windows.Controls.DataGridRow �� Triggers3  -System.Windows.Media.Effects.DropShadowEffect   Color   ShadowDepth   
BlurRadius��%�  &	        &	 U    �� 5   
   " ��6=   .R�5      �� $ Bd��5      #��z��5      
���   �#��z��5      $ 8��6E   $ 2q�5      ._�5      �� $	 PART_ContentHost��6   5      
 5      P� 5       � {� .  . �5      �� # � ��63   $ Bd��6u    � ���� 5      # � ��63    � ��5      P� 5       �@J�	IsFocused . . �5      ��  � �� �#D9DDDC63   5      P� 5       � ��$
 Auto��6+   . �5      ��  � ��$	 100��61   5      P� 5       � ��$
 Auto��6,   . �5      ��  � ��$ 20��62   5      5!      �� 5#   
   "  60   .�5$      ��  �@J�SnapsToDevicePixels . 64   �� 5%       � �� �#3B4D616+   �� 5&       � ��$ Normal"�6+   �� 5'       � ��
 �����6+   �� 5(       �@�ContextMenu 6,   Z�  5)      P� 5*       �@ AlternationIndex$ 1��66   . �5+      ��  � �� �	#FFD0D0E063   5,      P� 5-       �@J�IsMouseOver . . �5.      ��  � �� �#ef9d1063   �� 5/       � �� �#3B4D6163   50      P� 51       �@ 
IsSelected . . �52      ��  � �� �#D9DDDC63   53      P� 54       �@=�HasError . . �55      ��  �@J�Effect 56        57      $	 Red��6/   $ 0Q�6;   $ 20Q�6K   59      �� 5:       � ��$ 2q�68   �� 5;       � ��
 �  ��64   �� 5<       � ��
 ��  �63   �� 5=       � ��$ 12(�61   5>      5@      5A      .��5B      �  � ColumnDefinitions 5C   
   �� 5D      $
��200* �6   �� 5E      $
��600* �6   �� 5F      $
��200* �6    � RowDefinitions 5H   
   �� 5I      $
��0.3* �6   �� 5J      $
��0.5* �6   �� 5K      $��3* �6   �� 5L      $��* �6   .+�5N   
   �� ���#3B4D61$ 10��5P      $��3��6   $��1��6,   $��
0,9.8,0,10q�6<   �� 5Q   
   ���#3B4D61$ 10��5S      $��1��6   $��1��6,   � 5T   
   $��1��6   $��1��6    5U      �� 5V      $
��0.8* �6#   �� 5W      $
��2.7* �6#   �� 5X      $
��1.3* �6#   .+�5Z      �� $��Nama Jenis :��6   $
��Left=�6+   $	��Top=�6F   $��	15,15,0,0q�6^   $��0��5[      $��12(�6$   $��26��62   $��77��6>   
�������6I   �� 5\      -   5         ��Name$ keyword��5\   M   #��[  6   $
��Left=�6^   $��22��6y   $��0,20,0,0q�6�    �� TextWrapping$
 Wrap=�5]      $�� ��6*   $	��Top=�62   $	��325��6J   $��0��5^      $��1��6#   $��0q�63   �� 5_      -   5         ��Name$
 cari��5_   &   $��Search��6   $
��Left=�65   $	��Top=�6P   $��	10,20,0,0q�5`      $	��134��6(   $��22��64   $��2��6@   $��0��6P   $��12(�6]   ��5b      �� 5c      " ��6   .�5d      ��  � ��$ Fonts/#Dosis)�6C   �� 5e       � �� �#ef9d1067   �� 5f       � ��
 �����67   �� 5g       �@�Cursor$
 Hand��63   �� 5h       � �� 5i      �� 5j   "   " ��62   .R�5k   &   �� $ 8��6-   #��z��6>   $��1q�6h   ._�5l   *   �� $��Center=�6;   $��Center=�6X   5m   '   5n   #   5p       5q      P� 5r       �@J�IsMouseOver . . �5s   "   ��  � �� �#3B4D616?   5t      5v      5x      5y      &   System.Windows.Controls.DataGrid  5z   
   -   5          Name$ dgvJenis��5z   G      RowStyle# [ 6   $
��Left=�6Y   $	��300��6t   $	��Top=�6�   $	��600��6�   $��
200,35,0,0q�6�      ColumnWidth5  /System.Windows.Controls.DataGridLengthConverter$ * 6�   $��2��6�      CanUserAddRows .     
IsReadOnly . $��2��6/  � 5|   
   $��3��6   $��1��6    5}      �� 5~      $
��0.7* �6#   �� 5      $
��2.4* �6#   �� 5�      $
��0.6* �6#   �� 5�      $
��0.6* �6#    5�      �� 5�      $
��0.5* �6    �� 5�      $��* �6    �� 5�      $��* �6    �� 5�      $
��0.5* �6    .+�5�      �� $��
ID Jenis :��6   $
��Left=�6)   $	��Top=�6D   $��36,2,0,0q�6\   $��1��5�      $��0��6!   $��12(�61   $��26��6?   $��56��6K   
�������6V   �� 5�      $��Nama Jenis :��6   $
��Left=�6+   $	��Top=�6F   $��15,1,0,0q�6^   $��2��5�      $��0��6!   $��12(�61   $��26��6?   $��77��6K   
�������6V   �� 5�      -   5        $ id��5�   M   #��[  6   $
��Left=�6Y   $��22��6t   $��0,6,0,0q�6�   $
 Wrap=�6�   $�� ��5�      $	��Top=�6   $	��325��66   $��1��6B   $��1��5�      $��0q�6&   �� 5�      -   5        $
 nama��5�   M   #��[  6   $
��Left=�6[   $��22��6v   $��0,5,0,0q�6�   $
 Wrap=�5�      $�� ��6*   $	��Top=�62   $	��325��6J   $��2��6V   $��1��5�      $��0q�6&   �� 5�      -   5        $ insert��5�      $��Insert��5�      $
��Left=�5�      $	��Top=�5�      $��5,6,0,0q�5�      $��70��6&   $��20��61   $��2��5�      $��1��6%   $��12(�62   ���#ef9d10��5�      �� 5�      " ��6   .�5�      ��  � ��$ Fonts/#Dosis)�6C   �� 5�       � �� �#ef9d1067   �� 5�       � ��
 �����67   �� 5�       �@�Cursor$
 Hand��63   �� 5�       � �� 5�      �� 5�   "   " ��62   .R�5�   &   �� $ 8��6-   #��z��6>   $��1q�6h   ._�5�   *   �� $��Center=�6;   $��Center=�6X   5�   '   5�   #   5�       5�      P� 5�       �@J�IsMouseOver . . �5�   "   ��  � �� �#3B4D616?   5�      5�      5�      �� 5�      -   5        $ update��5�   &   $��Update��6   $
��Left=�67   $	��Top=�5�      $��0,6,0,0q�6-   $��70��6>   $��20��6I   $��3��6U   $��1��6e   $��12(�5�      ��5�      �� 5�      " ��6   .�5�      ��  � ��$ Fonts/#Dosis)�6C   �� 5�       � �� �#ef9d1067   �� 5�       � ��
 �����67   �� 5�       �@�Cursor$
 Hand��63   �� 5�       � �� 5�      �� 5�   "   " ��62   .R�5�   &   �� $ 8��6-   #��z��6>   $��1q�6h   ._�5�   *   �� $��Center=�6;   $��Center=�6X   5�   '   5�   #   5�       5�      P� 5�       �@J�IsMouseOver . . �5�   "   ��  � �� �#3B4D616?   5�      5�      5�      �� 5�      -	   5        $ delete��5�   &   $��Delete��6   $
��Left=�67   $	��Top=�6R   $��5,5,0,0q�5�      $��70��6&   $��20��61   $��2��6=   $��2��6M   $��12(�6Z   ��5�      �� 5�      " ��6   .�5�      ��  � ��$ Fonts/#Dosis)�6C   �� 5�       � �� �#ef9d1067   �� 5�       � ��
 �����67   �� 5�       �@�Cursor$
 Hand��63   �� 5�       � �� 5�      �� 5�   "   " ��62   .R�5�   &   �� $ 8��6-   #��z��6>   $��1q�6h   ._�5�   *   �� $��Center=�6;   $��Center=�6X   5�   '   5�   #   5�       5�      P� 5�       �@J�IsMouseOver . . �5�   "   ��  � �� �#3B4D616?   5�      5�      5�      �� 5�      -
   5        $ resets��5�   %   $��Reset��6   $
��Left=�66   $	��Top=�6Q   $��0,5,0,0q�5�      $��70��6&   $��20��61   $��3��6=   $��2��6M   $��12(�6Z   ��5�      �� 5�      " ��6   .�5�      ��  � ��$ Fonts/#Dosis)�6C   �� 5�       � �� �#ef9d1067   �� 5�       � ��
 �����67   �� 5�       �@�Cursor$
 Hand��63   �� 5�       � �� 5�      �� 5�   "   " ��62   .R�5�   &   �� $ 8��6-   #��z��6>   $��1q�6h   ._�5�   *   �� $��Center=�6;   $��Center=�6X   5�   '   5�   #   5�       5�      P� 5�       �@J�IsMouseOver . . �5�   "   ��  � �� �#3B4D616?   5�      5�      5�      5      � 5  
   ���#3B4D61$��
0,0,0,12.6q�5     $��3��6!    5     �� 5     �� 5     �� 5     �� 5     �� 5	     �� 5
     �� 5     �� 5     �� 5     �� 5     �� 5     �� 5     �� 5     �� 5     .+�5     �� -   5        $ 
btn_master��5     $��MASTER��5     $��1��5     
�������$��Center=�5     $��Center=�5     $��Fonts/#Dosis)�5     $��Medium"�5     $��0,0,10,0q�6%   ��5     �� 5     " ��6   .�5     ��  � ��$ Fonts/#Dosis)�6C   �� 5      � �� �#3B4D6167   �� 5       �@�Cursor$
 Hand��63   5!     ��5#     �� 5$     " ��6&   .R�5%     �� #��z��6!   ._�5&     �� $��Center=�6/   $��Center=�5'  !   5(     5)     5+     �� 5,     -   5        $ btn_customer��52     $��Customer��5,     
�������$��10(�5.     $��Medium"�5/     $��0,0,10,0q�50     $��2��51      J� RenderTransformOrigin$ 	0.503,0.5&�52  B   ��53     �� 54     " ��6   .�55     ��  � ��$ Fonts/#Dosis)�6C   �� 56      � �� �#3B4D6167   �� 57      �@�Cursor$
 Hand��63   58     ��5:     �� 5;     " ��6&   .R�5<     �� #��z��6!   ._�5=     �� $��Center=�6/   $��Center=�5>  !   5?     5@     5B     �� 5C     -   5        $ btn_supplier��5I     $��Supplier��5C     $��10(�5D     
�������$��Medium"�5F     $��0,0,10,0q�5G     $��3��5H     ��5K     �� 5L     " ��6   .�5M     ��  � ��$ Fonts/#Dosis)�6C   �� 5N      � �� �#3B4D6167   �� 5O      �@�Cursor$
 Hand��63    5P     P� 5Q      �@J�IsMouseOver . . �5R  "   ��  � �� �#ef9d106?   5S     5U     ��5W     �� 5X     " ��6&   .R�5Y     �� #��z��6!   ._�5Z     �� $��Center=�6/   $��Center=�5[  !   5\     5]     5_     �� 5`     -   5        $ btn_karyawan��5f     $��Karyawan��5`     $��10(�5a     
�������$��Medium"�5c     $��0,0,10,0q�5d     $��4��5e     ��5h     �� 5i     " ��6   .�5j     ��  � ��$ Fonts/#Dosis)�6C   �� 5k      � �� �#3B4D6167   �� 5l      �@�Cursor$
 Hand��63    5m     P� 5n      �@J�IsMouseOver . . �5o  "   ��  � �� �#ef9d106?   5p     5r     ��5t     �� 5u     " ��6&   .R�5v     �� #��z��6!   ._�5w     �� $��Center=�6/   $��Center=�5x  !   5y     5z     5|     �� 5}     -   5        $ btn_alat_musik��5�     $��
Alat Musik��5}     $��10(�5~     
�������$��Medium"�5�     $��0,0,10,0q�5�     $��5��5�     ��5�     �� 5�     " ��6   .�5�     ��  � ��$ Fonts/#Dosis)�6C   �� 5�      � �� �#3B4D6167   �� 5�      �@�Cursor$
 Hand��63    5�     P� 5�      �@J�IsMouseOver . . �5�  "   ��  � �� �#ef9d106?   5�     5�     ��5�     �� 5�     " ��6&   .R�5�     �� #��z��6!   ._�5�     �� $��Center=�6/   $��Center=�5�  !   5�     5�     5�     �� 5�     -   5        $ btn_aksesoris��5�     $��	Aksesoris��5�     $��10(�5�     
�������$��Medium"�5�     $��0,0,10,0q�5�     $��6��5�     ��5�     �� 5�     " ��6   .�5�     ��  � ��$ Fonts/#Dosis)�6C   �� 5�      � �� �#3B4D6167   �� 5�      �@�Cursor$
 Hand��63    5�     P� 5�      �@J�IsMouseOver . . �5�  "   ��  � �� �#ef9d106?   5�     5�     ��5�     �� 5�     " ��6&   .R�5�     �� #��z��6!   ._�5�     �� $��Center=�6/   $��Center=�5�  !   5�     5�     5�     �� 5�     -   5        $ btn_produsen��5�     $��Produsen��5�     $��10(�5�     
�������$��Medium"�5�     $��0,0,10,0q�5�     $��7��5�     ��5�     �� 5�     " ��6   .�5�     ��  � ��$ Fonts/#Dosis)�6C   �� 5�      � �� �#3B4D6167   �� 5�      �@�Cursor$
 Hand��63    5�     P� 5�      �@J�IsMouseOver . . �5�  "   ��  � �� �#ef9d106?   5�     5�     ��5�     �� 5�     " ��6&   .R�5�     �� #��z��6!   ._�5�     �� $��Center=�6/   $��Center=�5�  !   5�     5�     5�     �� 5�     -   5        $ 	btn_jenis��5�     $��
Jenis Alat��5�     $��10(�5�     
�������$��Medium"�5�     $��0,0,10,0q�5�     $��8��5�     ��5�     �� 5�     " ��6   .�5�     ��  � ��$ Fonts/#Dosis)�6C   �� 5�      � �� �#ef9d1067   �� 5�      �@�Cursor$
 Hand��63    5�     P� 5�      �@J�IsMouseOver . . �5�  "   ��  � �� �#ef9d106?   5�     5�     ��5�     �� 5�     " ��6&   .R�5�     �� #��z��6!   ._�5�     �� $��Center=�6/   $��Center=�5�  !   5�     5�     5�     �� 5�     -   5        $ 	btn_promo��5�     $��Promo��5�     $��10(�5�     
�������$��Medium"�5�     $��0,0,10,0q�5�     $��9��5�     ��5�     �� 5�     " ��6   .�5�     ��  � ��$ Fonts/#Dosis)�6C   �� 5�      � �� �#3B4D6167   �� 5�      �@�Cursor$
 Hand��63    5�     P� 5�      �@J�IsMouseOver . . �5�  "   ��  � �� �#ef9d106?   5�     5     ��5     �� 5     " ��6&   .R�5     �� #��z��6!   ._�5     �� $��Center=�6/   $��Center=�5  !   5     5	     5     �� 5     -   5        $ 
btn_member��5     $��Member��5     $��10(�5     
�������$��Medium"�5     $��0,0,10,0q�5     $��10��5     ��5     �� 5     " ��6   .�5     ��  � ��$ Fonts/#Dosis)�6C   �� 5      � �� �#3B4D6167   �� 5      �@�Cursor$
 Hand��63    5     P� 5      �@J�IsMouseOver . . �5  "   ��  � �� �#ef9d106?   5     5     ��5      �� 5!     " ��6&   .R�5"     �� #��z��6!   ._�5#     �� $��Center=�6/   $��Center=�5$  !   5%     5&     5(     �� 5*     -   5        $ LogOut��51     $
��Back��5*     
�������$��10(�5,     $��Medium"�5-     $��0,0,10,0q�5.     $��12��5/     $��2��50     ��53     �� 54     " ��6   .�55     ��  � ��$ Fonts/#Dosis)�6C   �� 56      � �� �#ef9d1067   �� 57      �@�Cursor$
 Hand��63    58     P� 59      �@J�IsMouseOver . . �5:  "   ��  � ��
 �  ��6?   5;     5=     ��5@     �� 5A     " ��6&   .R�5B     �� $��75��6!   $��15��6,   $ 8��5C     #��z��5D     ._�5E     �� $��Center=�6/   $��Center=�5F  !   5G     5H     5J     5L     �� 5M  
   ���#3B4D61$��90,87.8,-15,2q�5O     $ 20��5P     $��3��6   $��2��6,   $ 0.5,0.5&�6<   z�5Q     V� 5R     .~�5S     �� �� 5T     �� 5U       �� Angle$  53.233Q�6&   T� 5V     5W     5Y     �� 5Z  
   ���#6B7B8C$��58,27.8,-65,62q�5\     $ 20��5]     $��3��6   $��2��6,   $ 0.5,0.5&�6<   z�5^     V� 5_     .~�5`     �� �� 5a     �� 5b     $  52.082Q�6&   T� 5c     5d     5f     �� 5g  
   ���#EF9D10$��140,375,-50,90q�5i     $ 20��5j     $��2��6   $��2��6/   $ 0.5,0.5&�6<   $��2��6\   z�5k     V� 5l     .~�5m     �� �� 5n     �� 5o     $  51.268Q�6&   T� 5p     5q     5s     5t     5u     