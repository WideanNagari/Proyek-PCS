   M S B A M L   `   `   `  ����h  dSAPBusinessObjects.WPF.Viewer, Version=13.0.3500.0, Culture=neutral, PublicKeyToken=692fbea5521e1304tRclr-namespace:SAPBusinessObjects.WPF.Viewer;assembly=SAPBusinessObjects.WPF.ViewerSAPBusinessObjects.WPF.Viewer  5   	   F BProject PCS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:Project_PCSProject_PCS 5   	      Project_PCS.Nota   5   	   ]ViewerRclr-namespace:SAPBusinessObjects.WPF.Viewer;assembly=SAPBusinessObjects.WPF.Viewer  W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35n jSAPBusinessObjects.WPF.ViewerShared, Version=13.0.3500.0, Culture=neutral, PublicKeyToken=692fbea5521e1304R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   +localclr-namespace:Project_PCS     5   	   -   5          � Title$
  Nota��5   	   $	��700��6   $
��1200��6#    � WindowStyle$
 None=�5
   	    � 
ResizeMode$ NoResize=�5   	    � WindowStartupLocation$ CenterScreen=�5   	   .��5      �  � RowDefinitions 5   
   �� 5      $��25 �6   �� 5      $
��0.3* �6   �� 5      $��3* �6   �� 5      $
��0.5* �6   .+�5   
   � ���#3B4D61$��2��5       � ColumnDefinitions 5      �� 5      �� 5      �� 5      �� 5      �� 5      �� 5      �� 5      �� 5      �� 5       �� 5!      �� 5"      �� 5#      �� 5$      �� 5%      .+�5'      �� $
��NOTA��6   $��1��5(      
�������$��Center=�5*      $��Center=�5+      $��Fonts/#Dosis)�5,      $��Medium"�5-      5.      �� 50   
   -   5         ��Name$ judul��50      $
��Nota��6   $��Center=�6.   $��	5,0,4.6,0q�6K   $��Center=�6^   $��0��6y   $��1��6�   $
��Bold"�6�   $��24(�6�   $
��1184��6�   $��48��6�   $��Center=�6�   $��Center=�6�   8   2SAPBusinessObjects.WPF.Viewer.CrystalReportsViewer  51   
   -   5        $��cReport��51   &   $
��Left=�65   $	��520��6P   $��10,0,0,0q�6]   $	��Top=�6o   $
��1174��6�   $��2��6�   � 52   
   $��3��6    53      �� 54      $��3* �6#   �� 55      $��* �6#   �� 56      $��* �6#   .+�58      �� -   5         ��Name$ close��58   %   $��Close��6   $
��Left=�65   $	��Top=�6P   $	��200��6h   $��50��6t   $��20(�6�   $��2��6�   $��	29,20,0,0q�6�   ��59      �� 5:       �� 
TargetType" ��6   .�5;      �� 	 �� Property	 � ��
 �� Value$
 Fonts/#Dosis)�6C   �� 5<      	 � ��
 �#ef9d1067   �� 5=      	 � ��

 �����67   �� 5>      	 �@�Cursor$

 Hand��63   �� 5?      	 � ��
 5@      �� 5A   "    �� 
TargetType" ��62   .R�5B   &   ��  �� CornerRadius$ 8��6-   #��z��6>   $��1q�6h   ._�5C   *   �� $��Center=�6;   $��Center=�6X   5D   '   5E   #   5G       �� Triggers 5H      P� 5I       P� Property �@J�IsMouseOver P� Value . . �5J   "   �� 	 � ��

 �  ��6?   5K      5M      5O      �� 5P      -   5        $ print��5P   %   $��Print��6   $
��Left=�65   $	��Top=�6P   $	��200��6h   $��50��6t   $��20(�6�   $��1��6�   $��	29,20,0,0q�6�   ��5Q      �� 5R      " ��6   .�5S      �� 	 � ��$
 Fonts/#Dosis)�6C   �� 5T      	 � ��
 �#ef9d1067   �� 5U      	 � ��

 �����67   �� 5V      	 �@�Cursor$

 Hand��63   �� 5W      	 � ��
 5X      �� 5Y   "   " ��62   .R�5Z   &   �� $ 8��6-   #��z��6>   $��1q�6h   ._�5[   *   �� $��Center=�6;   $��Center=�6X   5\   '   5]   #   5_       5`      P� 5a       �@J�IsMouseOver . . �5b   "   �� 	 � ��
 �#0052546?   5c      5e      5g      5h      5i      5j      