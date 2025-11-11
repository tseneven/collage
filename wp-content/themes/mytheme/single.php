<?php get_header(); ?>

<main class="main_post">
    <section class="post_item">
        <?php if (have_posts()) : while (have_posts()) : the_post(); ?>
            <div class="post_item_content">
                <h1 class="blog_item_title"><?php the_title(); ?></h1>
                 <?php the_content(); ?>
                  <p>Опубликовано <?php the_time('F jS, Y'); ?></p>
                  <p>Автор: <?php the_author(); ?></p>
            </div>
        <?php endwhile; endif; ?>
    </section>
</main>

<?php get_footer(); ?>