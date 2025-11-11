<!DOCTYPE html>
<html lang="ru">

<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <link rel="stylesheet" href="<?php echo get_template_directory_uri() ?>/style.css?v=<?php echo time(); ?>">
    <?php wp_head() ?>
</head>

<body>

    <header class="header_section">
        <img src="<?php echo get_template_directory_uri(); ?> /image/logo.jpg" alt="logo" class="header_logo">
        <div class="search">
            <?php get_search_form(); ?>
        </div>
        <a href="<?php echo home_url(); ?>">Главная</a>
        <?php wp_nav_menu(array(
            'theme_location' => 'header-menu',
            'menu_class' => 'header_nav',
        )); ?>
    </header>