<template>
  <section class="row justify-content-around mt-2">
    <div class="col-11 text-center  rounded">
      <section class="row">
        <div class="col-12 text-light title-bg rounded-top">
          <slot name="title"></slot>
        </div>
        <div class="col-12 shadow rounded-bottom">
          <slot name="instructions"></slot>
        </div>

      </section>
    </div>
  </section>
</template>


<script>
import { AppState } from '../AppState';
import { computed, reactive, onMounted } from 'vue';
export default {
  setup() {
    return {}
  }
};
</script>


<style lang="scss" scoped>
.title-bg {
  background-color: #527360;
}
</style>