<?php get_header(); ?>

<main class="main_page">
    <section class="page_content">
        <?php while (have_posts()) :  the_post(); ?>
            <div class="page_item_content" id="post-<?php the_ID(); ?>" > 
                <h1 class="page_item_title"><?php the_title(); ?></h1>
                <?php the_content(); ?>
            </div>
        <?php endwhile; ?>
        
</section>
</main>

<?php get_footer(); ?>