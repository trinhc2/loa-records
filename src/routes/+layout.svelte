<script lang="ts">
	import '../app.css';
	import { AppBar } from '@skeletonlabs/skeleton';
	import { SettingsIcon, FileTextIcon } from 'svelte-feather-icons';

	let { children } = $props();
</script>

<div>
	<AppBar gridColumns="grid-cols-3" slotDefault="place-self-center" slotTrail="place-content-end">
		<svelte:fragment slot="lead">
			<div class="flex gap-3">
				<a type="button" class="appBarButton" href="/">
					<div class="flex items-center justify-between gap-2">
						<FileTextIcon />
						Records
					</div>
				</a>
			</div>
		</svelte:fragment>
		<svelte:fragment slot="trail">
			<a type="button" class="appBarButton" href="/settings">
				<div class="flex items-center justify-between gap-2">
					<SettingsIcon />
					Settings
				</div>
			</a>
		</svelte:fragment>
	</AppBar>
</div>

<div class="m-3">
	{@render children()}
</div>
