   M S B A M L   `   `   `  ����F  BProject PCS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:Project_PCSProject_PCS  5   	       Project_PCS.WelcomeScreen   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35n jSAPBusinessObjects.WPF.ViewerShared, Version=13.0.3500.0, Culture=neutral, PublicKeyToken=692fbea5521e1304R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   +localclr-namespace:Project_PCS     5   	     � Title$  WelcomeScreen��5   	   $	��450��6   $	��800��6,    � WindowStartupLocation$ CenterScreen=�68   ��5	      �� 5
   
    �� ImageSource$ Background.png��6   .��5      �  � RowDefinitions 5   
   �� 5      $��* �6   �� 5      $��3* �6   �� 5      $��* �6   .+�5   
   � $��0��6    � ColumnDefinitions 5      �� 5      $��2* �6#   �� 5      $
��0.5* �6#   �� 5      $
��1.5* �6#   �� 5      $
��0.5* �6#   �� 5      $��2* �6#   .+�5      �� $��60��6   $��50��6   $��Images/music.png��5       �� BitmapScalingMode$
 Fant=�5      $��	0,0,0,-30q�68   $��1��6K   �� 5      $��MUSELLA��6   $��40(�5      $
��Bold"�5       �� TextAlignment$ Center=�5       $��Center=�5!      $��Fonts/#Dosis)�5"      $��0,0,0,0q�5#      
�������$��55��65   $��2��6A   �� 5$      $��60��6   $��50��6   $��Images/music.png��5%      $
 Fant=�5&      $��	0,0,0,-30q�68   $��3��6K   5'      � 5(   
   -   5        $	��pb1��5(      $
��Left=�6!   $��35��6<   $��36,2,0,0q�6H   $	��Top=�6Z   $	��724��6r    � Minimum$ 0Q�6~    � Maximum$ 30Q�6�   ���	#FFEF9D10$��2��6�   	 � Value	 6�   �� *WorkerState+��5)      �� 5*      5,      5-      5.      