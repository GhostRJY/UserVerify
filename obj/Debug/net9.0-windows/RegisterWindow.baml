   M S B A M L   `   `   `  ����I  EUserVerify_WPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:UserVerify_WPFUserVerify_WPF  5   	   #    UserVerify_WPF.RegisterWindow   5   	   W SPresentationCore, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   .localclr-namespace:UserVerify_WPF     5   	     � Title$  RegisterWindow��5   	   $	��282��6    $	��358��6-    � 
ResizeMode$ NoResize=�69   ��5	      �� 5
   
    �� EndPoint$ 0.5,1&�6    �� 
StartPoint$ 0.5,0&�6/   .��5      �  � Color$ Black��6    � Offset$ 1Q�6)   � 5      $ 	#FF96C77C��6   $ 0.614Q�6-   5      .��5      �  � RowDefinitions 5   
   �� 5      .+�5   
   �� -   5         ��Name$ RegisterButtonRegWin��5      $��Register��6/   $
��Left=�6B   $��
20,202,0,0q�6]   $	��Top=�6q   $	��138��6�   6�   �� 5   
   -   5         ��Name$ LoginRegWin��5      $
��Left=�6'   $��	64,65,0,0q�6B   	 �� TextWrapping$
	 Wrap=�6U   $	��Top=�6i   $	��224��6�   
 J� RenderTransformOrigin$
 0.5,0.5&�6�   .��6�   z�5      V� 5      .~�5      �� �� 5      �� 5       �� Angle$ -0.029Q�6&   T� 5      5      .��5   '   5      �� 5   
   -   5        $ PasswordRegWin��5      $
��Left=�6*   $��
64,131,0,0q�6E   $
	 Wrap=�6Y   $	��Top=�6m   $	��224��6�   $
 0.5,0.5&�6�   .��6�   z�5      V� 5       .~�5!      �� �� 5"      �� 5#      $ -0.029Q�6&   T� 5$      5%      .��5&   '   5'      �� 5(   
   $
��Left=�6   $��	64,48,0,0q�6/   $
��Wrap=�6B   $��Login��6V   $	��Top=�6c   �� 5)   
   $
��Left=�6   $��
64,114,0,0q�6/   $
��Wrap=�6C   $��Password��6W   $	��Top=�6g   �� 5*   
   -   5        $ CancelButtonRegWin��5*      $��Cancel��6-   $
��Left=�6>   $��196,202,0,0q�6Y   $	��Top=�6n   $	��138��6�   6�   5,      5-      