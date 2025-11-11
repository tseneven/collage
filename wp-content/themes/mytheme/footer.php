<footer class = "footer_section">
    <div class="footer_copyright">
        © 2025 Neven. Все права защищены.
    </div>
    <a href = "<?php echo home_url(); ?>">Главная</a>
    <?php wp_nav_menu(array(
        'theme_location' => 'header-menu',
        'menu_class' => 'header_nav',
    )); ?>
</footer>
    <script src="https://code.jquery.com/jquery-3.7.1.js" integrity= 
"sha256-eKhayi8LEQwp4NKxN+CfCh+3qOVUtJn3QNZ0TciWLP4=" 
crossorigin="anonymous"> </script> 
<?php wp_footer()?>
</body>
</html>