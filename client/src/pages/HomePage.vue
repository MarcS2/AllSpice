<template>
  <div class="container-fluid">
    <section class="row justify-content-center">
      <div class="col-11 hero-bg shadow rounded">

      </div>
      <div class="col-10">
        <router-view></router-view>
        <!-- <section class="row justify-content-center">
          <div class="col-5 mt-3 shadow rounded-pill">
            <section class="row">
              <div class="col-4 text-center fs-5">
                My Recipes
              </div>
              <div class="col-4 text-center fs-5 border-bottom border-primary border-3">
                Home
              </div>
              <div class="col-4 text-center fs-5">
                Favorites
              </div>
            </section>
          </div>
        </section> -->
      </div>
    </section>
  </div>
</template>

<script>
export default {
  setup() {
    return {

    }
  }
}
</script>

<style scoped lang="scss">
.home {
  display: grid;
  height: 80vh;
  place-content: center;
  text-align: center;
  user-select: none;

  .home-card {
    width: clamp(500px, 50vw, 100%);

    >img {
      height: 200px;
      max-width: 200px;
      width: 100%;
      object-fit: contain;
      object-position: center;
    }
  }
}

.hero-bg {
  background-image: url('https://images.unsplash.com/photo-1583330613963-19051a45ee4c?q=80&w=1974&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D');
  background-size: cover;
  background-position: center;
  height: 35dvh;

}
</style>
